using CommunityToolkit.Mvvm.ComponentModel;
using SortVisualizer.Models;
using SortVisualizer.Services;
using System;

namespace SortVisualizer.ViewModels;

public partial class VisualizationViewModel : ObservableObject
{
    public PlayerViewModel Player { get; }
    public SortAlgorithm Algorithm { get; }

    [ObservableProperty] private string description = string.Empty;
    [ObservableProperty] private Complexity complexity = new Complexity();

    public VisualizationViewModel(Func<SortAlgorithm, PlayerViewModel> playerFactory, SortAlgorithm algorithm)
    {
        Algorithm = algorithm;
        Player = playerFactory(algorithm);

        LoadAlgorithmInfo(algorithm);
    }
    private void LoadAlgorithmInfo(SortAlgorithm algorithm)
    {
        var info = AlgorithmInfoProvider.Get(algorithm.ToString());
        Description = info.AdditionalInfo.Description;
        Complexity = info.AdditionalInfo.Complexity;
    }
}
