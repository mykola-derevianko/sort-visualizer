using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ISortItemGenerator _generator;
    private readonly BubbleSortCommandBuilder _bubbleBuilder = new BubbleSortCommandBuilder();

    private CommandPlayer? _player;
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


    //KOSTYL` ZOSKIII,

    partial void OnSpeedChanged(double value)
    {
        if (_player != null && IsPlaying)
        {
            Cancel();
        }
    }


    public MainViewModel(ISortItemGenerator generator)
    {
        _generator = generator;
    }


    //KOSTYL` ZOSKIII, 
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

        StartQuickSort();
    }



    [RelayCommand]
    private void StepForward()
    {
        if (_player == null) return;
        _player.StepForward(Items);
        CurrentStep = _player.CurrentStep;
    }

    [RelayCommand]
    private void StepBackward()
    {
        if (_player == null) return;
        _player.StepBackward(Items);
        CurrentStep = _player.CurrentStep;
    }

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
        catch (OperationCanceledException) { }
        finally
        {
            IsPlaying = false;
            CurrentStep = _player?.CurrentStep ?? 0;
        }
    }


    [RelayCommand]
    private void StartBubbleSort()
    {
        Cancel();
        var commands = _bubbleBuilder.Build(Items);
        _player = new CommandPlayer(commands, step => CurrentStep = step);
        CurrentStep = 0;
        TotalSteps = _player.TotalSteps;
    }

    private readonly QuickSortCommandBuilder _quickSortBuilder = new();

    [RelayCommand]
    private void StartQuickSort()
    {
        Cancel();
        var commands = _quickSortBuilder.Build(Items);
        _player = new CommandPlayer(commands, step => CurrentStep = step);
        CurrentStep = 0;
        TotalSteps = _player.TotalSteps;
    }

    [RelayCommand]
    private void TogglePlay()
    {
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
        if (_player == null) return;
        _player.JumpToStep(index, Items);
        CurrentStep = _player.CurrentStep;
    }

    private void Cancel()
    {
        if (_cts is not null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        _player?.Pause();
        IsPlaying = false;
    }
}
