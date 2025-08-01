using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using SortVisualizer.Sorting.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISortItemGenerator _generator;
    private readonly QuickSortCommandBuilder _quickSortBuilder = new();
    private readonly BubbleSortCommandBuilder _bubbleSortBuilder = new();

    private SortPlaybackService? _playbackService;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private ObservableCollection<SortItem> items = new();

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private int currentStep;

    [ObservableProperty]
    private int totalSteps;

    [ObservableProperty]
    private double speed = 1.0;

    public MainViewModel(ISortItemGenerator generator)
    {
        _generator = generator;
    }

    partial void OnSpeedChanged(double value)
    {
        if (_playbackService != null && IsPlaying)
        {
            _playbackService.Cancel();
            _ = PlayAsync(); // Restart at new speed
        }
    }

    [RelayCommand]
    private void Generate(GenerateType type)
    {
        Cancel();

        Items = type switch
        {
            GenerateType.Random => _generator.GenerateRandom(),
            GenerateType.Reversed => _generator.GenerateReversed(),
            GenerateType.NearlySorted => _generator.GenerateNearlySorted(),
            _ => _generator.GenerateRandom(),
        };

        StartQuickSort(); // Or switch to Bubble if preferred
    }

    [RelayCommand]
    private void StartQuickSort() => SetupSort(_quickSortBuilder.Build(Items));

    [RelayCommand]
    private void StartBubbleSort() => SetupSort(_bubbleSortBuilder.Build(Items));

    private void SetupSort(IEnumerable<ISortCommand> commands)
    {
        Cancel();

        _playbackService = new SortPlaybackService(commands);
        _playbackService.OnStepChanged += step => CurrentStep = step;
        _playbackService.OnPlayingChanged += playing => IsPlaying = playing;

        CurrentStep = 0;
        TotalSteps = _playbackService.TotalSteps;
    }

    [RelayCommand]
    private void StepForward()
    {
        _playbackService?.StepForward(Items);
    }

    [RelayCommand]
    private void StepBackward()
    {
        _playbackService?.StepBackward(Items);
    }

    [RelayCommand]
    private async Task PlayAsync()
    {
        if (_playbackService == null || IsPlaying) return;

        _cts = new CancellationTokenSource();

        try
        {
            await _playbackService.PlayAsync(Items, Speed, _cts.Token);
        }
        catch (OperationCanceledException) { }
    }

    [RelayCommand]
    private void TogglePlay()
    {
        if (_playbackService == null) return;

        if (IsPlaying)
            _playbackService.Cancel();
        else
            _ = PlayAsync();
    }

    [RelayCommand]
    private void JumpToStep(int index)
    {
        _playbackService?.JumpToStep(index, Items);
    }

    private void Cancel()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _playbackService?.Cancel();
    }
}
