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

    private readonly DispatcherTimer _debounceTimer;
    private readonly SortManagerService _sortManager;

    [ObservableProperty]
    private string rawInput = "";

    public ObservableCollection<SortItem> ParsedItems { get; private set; } = new();

    public event EventHandler<ObservableCollection<SortItem>>? OnItemsChanged;

    private int _maxElementsValue = 50;
    public int MaxElementsValue
    {
        get => _maxElementsValue;
        set
        {
            int newValue = value > 50 ? 50 : value < 2 ? 2 : value;
            SetProperty(ref _maxElementsValue, newValue);
        }
    }

    public string MaxElementsValueText
    {
        get => _maxElementsValue.ToString();
        set
        {
            if (int.TryParse(value, out int num))
                MaxElementsValue = num;
            else if (string.IsNullOrWhiteSpace(value))
                MaxElementsValue = 0;
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
            ParseAndNotify();
        };
    }

    partial void OnRawInputChanged(string value) => RestartDebounceTimer();

    private void RestartDebounceTimer()
    {
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private void ParseAndNotify()
    {
        var sanitized = Sanitize(RawInput);
        ParsedItems = ParseSortItems(sanitized);
        OnItemsChanged?.Invoke(this, ParsedItems);
    }

    [RelayCommand]
    public void GenerateData(GenerateType generateType)
    {
        ParsedItems = _sortManager.GenerateItems(generateType, (int)MaxElementsValue!);
        RawInput = ConvertParsedItemsToString(ParsedItems);
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