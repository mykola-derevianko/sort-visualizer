using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.ViewModels;

public partial class PlayerViewModel : ObservableObject
{
    private readonly SortManagerService _sortManager;
    private readonly SortAlgorithm _sortAlgorithm;
    public SettingsPopupViewModel settingsPopupVM { get; }

    private CommandPlayer? _player;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private ObservableCollection<SortItem> items = new();

    [ObservableProperty] private bool isPlaying;

    [ObservableProperty] private int currentStep;

    [ObservableProperty] private int totalSteps;

    [ObservableProperty] private double speed = 1.0;

    [ObservableProperty] private bool isMenuOpen;

    [ObservableProperty] private bool isPseudoCodeExpanded = false;

    [ObservableProperty] 
    private ObservableCollection<PseudoCodeLine> pseudoCodeLines = new();

    // Add this property to control progress bar interactivity
    [ObservableProperty] 
    private bool isProgressBarInteractive = true;

    public PlayerViewModel(SortManagerService sortManager, SettingsPopupViewModel settingsPopup, SortAlgorithm algorithm)
    {
        _sortManager = sortManager;
        _sortAlgorithm = algorithm;
        settingsPopupVM = settingsPopup;

        // Initialize pseudo code
        var pseudoCode = PseudoCodeProvider.GetPseudoCode(algorithm);
        PseudoCodeLines = new ObservableCollection<PseudoCodeLine>(pseudoCode);

        settingsPopupVM.OnItemsChanged += (sender, items) =>
        {
            if (items != null && items.Count > 0)
            {
                Cancel();
                Items = new ObservableCollection<SortItem>(items.Select(v => new SortItem(v)));
                StartSort(_sortAlgorithm);
            }
        };

        Generate(GenerateType.Random, algorithm);
    }

    // Update IsProgressBarInteractive when IsPlaying changes
    partial void OnIsPlayingChanged(bool value)
    {
        IsProgressBarInteractive = !value;
    }

    partial void OnCurrentStepChanged(int value)
    {
        UpdatePseudoCodeHighlighting();
    }

    private void UpdatePseudoCodeHighlighting()
    {
        // Clear all highlights
        foreach (var line in PseudoCodeLines)
        {
            line.IsHighlighted = false;
        }

        // Highlight current line if available
        if (_player != null && CurrentStep > 0 && CurrentStep <= _player.TotalSteps)
        {
            var currentCommand = _player.GetCommandAtStep(CurrentStep - 1);
            if (currentCommand != null && currentCommand.PseudoCodeLineNumber >= 0 && 
                currentCommand.PseudoCodeLineNumber < PseudoCodeLines.Count)
            {
                PseudoCodeLines[currentCommand.PseudoCodeLineNumber].IsHighlighted = true;
            }
        }
    }

    partial void OnSpeedChanged(double value)
    {
        if (_player != null && IsPlaying)
        {
            Cancel();
            _ = PlayAsync();
        }
    }

    private void Generate(GenerateType type, SortAlgorithm algorithm)
    {
        Cancel();
        Items = _sortManager.GenerateItems(type);
        StartSort(algorithm);
    }

    [RelayCommand]
    private void StartSort(SortAlgorithm algorithm)
    {
        Cancel();

        _player = _sortManager.StartSort(Items, algorithm, step => CurrentStep = step);

        CurrentStep = 0;
        TotalSteps = _player.TotalSteps;
    }

    [RelayCommand]
    private void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
    }

    [RelayCommand]
    private async Task StepForward()
    {
        if (_player == null) return;
        if (IsPlaying)
        {
            Cancel();
        }

        _player.StepForward(Items);
        CurrentStep = _player.CurrentStep;

        if (CurrentStep == _player.TotalSteps)
        {
            await PlayFinishAnimationAsync();
        }
    }

    [RelayCommand]
    private void StepBackward()
    {
        if (_player == null) return;

        if (IsPlaying)
        {
            Cancel();
        }

        _player.StepBackward(Items);
        CurrentStep = _player.CurrentStep;
    }

    [RelayCommand]
    private async Task PlayAsync()
    {
        if (_player == null || IsPlaying)
            return;

        _cts = new CancellationTokenSource();
        IsPlaying = true;

        try
        {
            await _player.PlayAsync(Items, _cts.Token, Speed);
            if(_player.CurrentStep == _player.TotalSteps)
            {
                await PlayFinishAnimationAsync();
            }
            
        }
        catch (OperationCanceledException){}
        finally
        {
            IsPlaying = false;
            CurrentStep = _player?.CurrentStep ?? 0;
        }
    }

    [RelayCommand]
    private void TogglePlay()
    {
        if (_player == null)
            return;

        if (IsPlaying)
        {
            Cancel();
        }
        else
        {
            _ = PlayAsync();
        }
    }

    [RelayCommand]
    private void JumpToStep(int index)
    {
        _player?.JumpToStep(index, Items);
        CurrentStep = _player?.CurrentStep ?? CurrentStep;
    }

    [RelayCommand]
    private void GenerateData(GenerateType generateType)
    {
        Generate(generateType, _sortAlgorithm);
    }

    [RelayCommand]
    public void Cancel()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        _player?.Pause();
        IsPlaying = false;
    }

    [RelayCommand]
    private void Restart()
    {
        Cancel();
        JumpToStep(0);
    }

    private async Task PlayFinishAnimationAsync()
    {
        foreach (var item in Items) {
            item.IsPivot = false;
            item.IsComparing = false;
            item.IsSwapping = false;
        }
        foreach (var item in Items)
        {
            foreach (var i in Items)
                i.IsPivot = false;

            item.IsPivot = true;
            await Task.Delay(30);
        }

        foreach (var item in Items)
            item.IsPivot = false;
    }
}
