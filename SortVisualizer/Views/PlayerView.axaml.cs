using Avalonia.Controls;
using Avalonia.Input;
using SortVisualizer.ViewModels;

namespace SortVisualizer.Views;

public partial class PlayerView : UserControl
{
    public PlayerView()
    {
        InitializeComponent();
        ProgressSlider.PointerCaptureLost += ProgressSlider_PointerCaptureLost;


    }
    private void ProgressSlider_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DataContext is PlayerViewModel vm && sender is Slider slider)
        {
            vm.JumpToStepCommand.Execute((int)slider.Value);
        }
    }

}
