using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Sorting.Commands
{
    public class SelectCommand : ISortCommand
    {
        private readonly int _index;

        public SelectCommand(int index) => _index = index;

        public void Execute(IList<SortItem> items) => items[_index].IsSelected = true;

        public void Highlight(IList<SortItem> items)
        {
            items[_index].IsSelected = true;   
        }

        public void Undo(IList<SortItem> items) => items[_index].IsSelected = false;
    }

}
