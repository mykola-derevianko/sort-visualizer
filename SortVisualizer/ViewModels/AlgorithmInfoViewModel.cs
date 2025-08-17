using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;
using SortVisualizer.Services;
using System.Collections.Generic;

namespace SortVisualizer.ViewModels;

public partial class AlgorithmInfoViewModel : ObservableObject
{
    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string description = string.Empty;
    [ObservableProperty] private string codeSnippet = string.Empty;
    [ObservableProperty] private List<string> pros = new();
    [ObservableProperty] private List<string> cons = new();

    public IRelayCommand VisualizeCommand { get; }

    private readonly MainViewModel _main;

    public AlgorithmInfoViewModel(SortAlgorithm algorithm, MainViewModel main)
    {
        _main = main;
        VisualizeCommand = new RelayCommand(() => _main.ShowVisualization(algorithm));

        LoadAlgorithmInfo(algorithm);
    }

    private void LoadAlgorithmInfo(SortAlgorithm algorithm)
    {
        var info = AlgorithmInfoProvider.Get(algorithm.ToString());
        Name = info.Name;
        Description = info.Description;
        CodeSnippet = info.CodeSnippet;
        Pros = info.Pros ?? new List<string>();
        Cons = info.Cons ?? new List<string>();
    }
}
