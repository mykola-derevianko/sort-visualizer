using CommunityToolkit.Mvvm.ComponentModel;
using SortVisualizer.ViewModels;

namespace SortVisualizer.ViewModels;

public class MainViewModel : ObservableObject
{
    public PlayerViewModel Player { get; }

    public MainViewModel(PlayerViewModel player)
    {
        Player = player;
    }
}

