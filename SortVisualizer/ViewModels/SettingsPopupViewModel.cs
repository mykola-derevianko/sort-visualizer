using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SortVisualizer.ViewModels;
public partial class SettingsPopupViewModel : ObservableObject
{
    private const double DebounceIntervalSeconds = 0.5;
    private const int MaxElementsLimit = 60;
    private const int MinElementsLimit = 1;
    private const int DefaultMaxElements = 50;

    private readonly DispatcherTimer _debounceTimer;
    private readonly SortManagerService _sortManager;

    [ObservableProperty]
    private string rawInput = "";

    [ObservableProperty]
    private string rawInputError = "";

    [ObservableProperty]
    private bool hasRawInputError = false;

    [ObservableProperty]
    private string maxElementsError = "";

    [ObservableProperty]
    private bool hasMaxElementsError = false;

    public ObservableCollection<SortItem> ParsedItems { get; private set; } = new();

    public event EventHandler<ObservableCollection<SortItem>>? OnItemsChanged;

    private int _maxElementsValue = DefaultMaxElements;

    public int MaxElementsValue
    {
        get => _maxElementsValue;
        set
        {
            int newValue = Math.Clamp(value, MinElementsLimit, MaxElementsLimit);
            if (SetProperty(ref _maxElementsValue, newValue))
            {
                OnPropertyChanged(nameof(MaxElementsValueText));
                ValidateMaxElements();
            }
        }
    }

    public string MaxElementsValueText
    {
        get => _maxElementsValue.ToString();
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                MaxElementsError = "This field is required";
                HasMaxElementsError = true;
                return;
            }

            if (!int.TryParse(value, out int num))
            {
                MaxElementsError = "Please enter a valid number";
                HasMaxElementsError = true;
                return;
            }

            if (num < MinElementsLimit || num > MaxElementsLimit)
            {
                MaxElementsError = $"Number must be between {MinElementsLimit} and {MaxElementsLimit}";
                HasMaxElementsError = true;
                return;
            }

            MaxElementsError = "";
            HasMaxElementsError = false;
            MaxElementsValue = num;
        }
    }


    public SettingsPopupViewModel(SortManagerService sortManager)
    {
        _sortManager = sortManager;

        _debounceTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(DebounceIntervalSeconds)
        };
        _debounceTimer.Tick += (_, __) =>
        {
            _debounceTimer.Stop();
            ValidateAndParseInput();
        };
    }

    partial void OnRawInputChanged(string value) => RestartDebounceTimer();

    private void RestartDebounceTimer()
    {
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private void ValidateAndParseInput()
    {
        if (string.IsNullOrWhiteSpace(RawInput))
        {
            RawInputError = "";
            HasRawInputError = false;
            ParsedItems = new ObservableCollection<SortItem>();
            OnItemsChanged?.Invoke(this, ParsedItems);
            return;
        }

        // Check for invalid characters (anything that's not a digit or whitespace)
        if (Regex.IsMatch(RawInput, @"[^\d\s]"))
        {
            RawInputError = "Only numbers and spaces are allowed";
            HasRawInputError = true;
            return;
        }

        var sanitized = Sanitize(RawInput);
        var parsedItems = ParseSortItems(sanitized);

        // Validate number ranges
        var invalidNumbers = parsedItems.Where(item => item.Value < MinElementsLimit || item.Value > MaxElementsLimit).ToList();
        if (invalidNumbers.Any())
        {
            RawInputError = $"All numbers must be between {MinElementsLimit} and {MaxElementsLimit}";
            HasRawInputError = true;
            return;
        }

        // Clear error if validation passes
        RawInputError = "";
        HasRawInputError = false;

        ParsedItems = parsedItems;
        MaxElementsValue = ParsedItems.Count;
        OnItemsChanged?.Invoke(this, ParsedItems);
    }

    private void ValidateMaxElements()
    {
        if (_maxElementsValue < MinElementsLimit || _maxElementsValue > MaxElementsLimit)
        {
            MaxElementsError = $"Number must be between {MinElementsLimit} and {MaxElementsLimit}";
            HasMaxElementsError = true;
        }
        else
        {
            MaxElementsError = "";
            HasMaxElementsError = false;
        }
    }

    [RelayCommand]
    public void GenerateData(GenerateType generateType)
    {
        if (HasMaxElementsError)
            return;

        ParsedItems = _sortManager.GenerateItems(generateType, (int)MaxElementsValue!);
        RawInput = ConvertParsedItemsToString(ParsedItems);
        RawInputError = "";
        HasRawInputError = false;
        OnItemsChanged?.Invoke(this, ParsedItems);
    }

    private static string Sanitize(string input)
    {
        var cleaned = Regex.Replace(input, @"[^\d\s]", "");
        cleaned = Regex.Replace(cleaned, @"\s+", " ");
        return cleaned.Trim();
    }

    private static ObservableCollection<SortItem> ParseSortItems(string input)
    {
        var list = new ObservableCollection<SortItem>();
        if (string.IsNullOrWhiteSpace(input))
            return list;

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            if (int.TryParse(part, out int num))
                list.Add(new SortItem { Value = num });
        }
        return list;
    }

    private string ConvertParsedItemsToString(ObservableCollection<SortItem> items)
    {
        return string.Join(" ", items.Select(item => item.Value));
    }
}