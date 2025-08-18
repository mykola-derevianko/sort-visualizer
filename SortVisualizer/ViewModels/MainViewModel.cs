using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using System;
using System.Collections.Generic;

namespace SortVisualizer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object? currentViewModel;

    [ObservableProperty]
    private SortAlgorithm selectedAlgorithm;

    private readonly Func<SortAlgorithm, VisualizationViewModel> _visualizationFactory;

    public MainViewModel(Func<SortAlgorithm, VisualizationViewModel> visualizationFactory)
    {
        _visualizationFactory = visualizationFactory;

        // Show initial info page
        CurrentViewModel = new AlgorithmInfoViewModel(SortAlgorithm.BubbleSort, this);
    }

    partial void OnSelectedAlgorithmChanged(SortAlgorithm value)
    {
        CurrentViewModel = new AlgorithmInfoViewModel(value, this);
    }
    public void ShowVisualization(SortAlgorithm algorithm)
    {
        SelectedAlgorithm = algorithm;
        CurrentViewModel = _visualizationFactory(algorithm);
    }

    public IEnumerable<SortAlgorithm> SortAlgorithms { get; } = new[]
    {
        SortAlgorithm.BubbleSort,
        SortAlgorithm.QuickSort,
        SortAlgorithm.InsertionSort,
        SortAlgorithm.SelectionSort
    };
}
