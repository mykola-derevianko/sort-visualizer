using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Services
{
    public interface ISortItemGenerator
    {
        ObservableCollection<SortItem> GenerateRandom();
        ObservableCollection<SortItem> GenerateReversed();
        ObservableCollection<SortItem> GenerateNearlySorted();
    }

}
