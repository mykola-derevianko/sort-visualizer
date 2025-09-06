using CommunityToolkit.Mvvm.ComponentModel;

namespace SortVisualizer.Models
{
    public partial class PseudoCodeLine : ObservableObject
    {
        [ObservableProperty] private string text = string.Empty;
        [ObservableProperty] private bool isHighlighted;
        [ObservableProperty] private int indentLevel;

        public PseudoCodeLine(string text, int indentLevel = 0)
        {
            Text = text;
            IndentLevel = indentLevel;
        }
    }
}