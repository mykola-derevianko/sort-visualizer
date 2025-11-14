using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;

namespace SortVisualizer.Sorting
{
    public interface ISortCommandBuilder
    {
        List<ISortCommand> Build(IList<SortItem> items);
        SortAlgorithm Algorithm { get; }
    }
}
