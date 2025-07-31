using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Sorting.Commands
{
    public interface ISortCommand
    {
        void Execute(IList<SortItem> items);
        void Undo(IList<SortItem> items);
    }

}
