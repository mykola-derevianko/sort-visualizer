using CommunityToolkit.Mvvm.ComponentModel;

namespace SortVisualizer.Models
{
    public partial class SortItem : ObservableObject
    {
        [ObservableProperty] private int value;

        [ObservableProperty] private bool isComparing;

        [ObservableProperty] private bool isSwapping;

        [ObservableProperty] private bool isPivot;

        [ObservableProperty] private bool isSelected;

        public SortItem() { }

        public SortItem(SortItem other)
        {
            Value = other.Value;
            IsComparing = other.IsComparing;
            IsSwapping = other.IsSwapping;
            IsPivot = other.IsPivot;
            IsSelected = other.IsSelected;
        }
    }

}
