using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Sorting.Commands
{
    public class SetPivotCommand : ISortCommand
    {
        private readonly int _pivotIndex;

        public SetPivotCommand(int pivotIndex)
        {
            _pivotIndex = pivotIndex;
        }

        public void Execute(IList<SortItem> items)
        {
            foreach (var item in items)
                item.IsPivot = false;

            items[_pivotIndex].IsPivot = true;
        }

        public void Undo(IList<SortItem> items)
        {
            items[_pivotIndex].IsPivot = false;
        }
    }

}
