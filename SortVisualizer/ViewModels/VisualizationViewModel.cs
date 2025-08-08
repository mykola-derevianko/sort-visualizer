using CommunityToolkit.Mvvm.ComponentModel;
using SortVisualizer.Models;
using System;

namespace SortVisualizer.ViewModels;

public partial class VisualizationViewModel : ObservableObject
{
    public PlayerViewModel Player { get; }
    public SortAlgorithm Algorithm { get; }

    public VisualizationViewModel(Func<SortAlgorithm, PlayerViewModel> playerFactory, SortAlgorithm algorithm)
    {
        Algorithm = algorithm;
        Player = playerFactory(algorithm);
    }
}
