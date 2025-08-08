using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.ViewModels;

public partial class PlayerViewModel : ObservableObject
{
    private readonly ISortManagerService _sortManager;
    private readonly SortAlgorithm _sortAlgorithm;
    private CommandPlayer? _player;
    private CancellationTokenSource? _cts;

    [ObservableProperty]
    private ObservableCollection<SortItem> items = new();

    [ObservableProperty] private bool isPlaying;

    [ObservableProperty] private int currentStep;

    [ObservableProperty] private int totalSteps;

    [ObservableProperty] private double speed = 1.0;

    public PlayerViewModel(ISortManagerService sortManager, SortAlgorithm algorithm)
    {
        _sortManager = sortManager;
        _sortAlgorithm = algorithm;
        Generate(GenerateType.Random, algorithm);
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
        StartSort(algorithm); // Default algorithm
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
    private async Task StepForward()
    {
        if (_player == null)
            return;

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

    private void Cancel()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _player?.Pause();
        IsPlaying = false;
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
