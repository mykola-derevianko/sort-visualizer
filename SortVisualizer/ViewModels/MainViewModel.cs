using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using System;

namespace SortVisualizer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object? currentViewModel;

    private readonly Func<SortAlgorithm, VisualizationViewModel> _visualizationFactory;

    public MainViewModel(Func<SortAlgorithm, VisualizationViewModel> visualizationFactory)
    {
        _visualizationFactory = visualizationFactory;

        // Show initial info page
        CurrentViewModel = new AlgorithmInfoViewModel(SortAlgorithm.BubbleSort, this);
    }

    [RelayCommand]
    private void ShowSortInfo(SortAlgorithm algorithm)
    {
        CurrentViewModel = new AlgorithmInfoViewModel(algorithm, this);
    }

    public void ShowVisualization(SortAlgorithm algorithm)
    {
        CurrentViewModel = _visualizationFactory(algorithm);
    }
}
