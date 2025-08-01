using SortVisualizer.Models;
using System.Collections.ObjectModel;

namespace SortVisualizer.Services
{
    public interface ISortItemGenerator
    {
        ObservableCollection<SortItem> GenerateRandom();
        ObservableCollection<SortItem> GenerateReversed();
        ObservableCollection<SortItem> GenerateNearlySorted();
    }

}
