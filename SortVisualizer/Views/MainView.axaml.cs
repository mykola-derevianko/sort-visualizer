using Avalonia.Controls;
using Avalonia.Input;
using SortVisualizer.ViewModels;

namespace SortVisualizer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        ProgressSlider.PointerCaptureLost += ProgressSlider_PointerCaptureLost;


    }
    private void ProgressSlider_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DataContext is MainViewModel vm && sender is Slider slider)
        {
            vm.JumpToStepCommand.Execute((int)slider.Value);
        }
    }

}
