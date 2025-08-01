using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISortManagerService _sortManager;
    private CommandPlayer? _player;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private ObservableCollection<SortItem> items = new();

    [ObservableProperty] private bool isPlaying;

    [ObservableProperty] private int currentStep;

    [ObservableProperty] private int totalSteps;

    [ObservableProperty] private double speed = 1.0;

    public MainViewModel(ISortManagerService sortManager)
    {
        _sortManager = sortManager;
    }

    partial void OnSpeedChanged(double value)
    {
        if (_player != null && IsPlaying)
        {
            Cancel();
            _ = PlayAsync(); // Restart playback at new speed
        }
    }

    [RelayCommand]
    private void Generate(GenerateType type)
    {
        Cancel();

        Items = _sortManager.GenerateItems(type);
        StartSort(SortAlgorithm.QuickSort); // Default algorithm
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
    private void StepForward()
    {
        _player?.StepForward(Items);
        CurrentStep = _player?.CurrentStep ?? CurrentStep;
    }

    [RelayCommand]
    private void StepBackward()
    {
        _player?.StepBackward(Items);
        CurrentStep = _player?.CurrentStep ?? CurrentStep;
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

    private void Cancel()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _player?.Pause();
        IsPlaying = false;
    }
}
