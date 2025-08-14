using Avalonia.Controls;
using Avalonia.Input;
using SortVisualizer.ViewModels;
using System.Threading.Tasks;

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
            //Cancel is not working proprly! TODO: make this thing work
            vm.JumpToStepCommand.Execute((int)slider.Value);
        }
    }

}
