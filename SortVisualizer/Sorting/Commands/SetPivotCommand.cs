using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class SetPivotCommand : ISortCommand
    {
        private readonly int _pivotIndex;
        
        public int PseudoCodeLineNumber { get; }

        public SetPivotCommand(int pivotIndex, int pseudoCodeLineNumber = -1)
        {
            _pivotIndex = pivotIndex;
            PseudoCodeLineNumber = pseudoCodeLineNumber;
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
