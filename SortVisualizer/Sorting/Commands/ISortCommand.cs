using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public interface ISortCommand
    {
        void Execute(IList<SortItem> items);
        void Undo(IList<SortItem> items);
    }

}
