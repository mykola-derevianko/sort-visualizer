using SortVisualizer.Models;
using SortVisualizer.Sorting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Services
{
    public interface ISortManagerService
    {
        ObservableCollection<SortItem> GenerateItems(GenerateType type);
        CommandPlayer StartSort(ObservableCollection<SortItem> items, SortAlgorithm algorithm, Action<int>? onStepChanged);
    }

}
