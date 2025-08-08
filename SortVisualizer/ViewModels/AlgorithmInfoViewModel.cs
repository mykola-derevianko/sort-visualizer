using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;

namespace SortVisualizer.ViewModels;

public partial class AlgorithmInfoViewModel : ObservableObject
{
    public IRelayCommand VisualizeCommand { get; }

    private readonly MainViewModel _main;

    public AlgorithmInfoViewModel(SortAlgorithm algorithm, MainViewModel main)
    {
        _main = main;
        VisualizeCommand = new RelayCommand(() => _main.ShowVisualization(algorithm));
    }
}
