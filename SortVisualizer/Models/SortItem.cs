// Models/SortItem.cs

using CommunityToolkit.Mvvm.ComponentModel;

namespace SortVisualizer.Models
{
    public partial class SortItem : ObservableObject
    {
        [ObservableProperty]
        private int value;

        [ObservableProperty]
        private bool isComparing;

        [ObservableProperty]
        private bool isSwapping;
    }

}
