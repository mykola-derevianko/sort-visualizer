using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class SelectCommand : ISortCommand
    {
        private readonly int _index;
        
        public int PseudoCodeLineNumber { get; }

        public SelectCommand(int index, int pseudoCodeLineNumber = -1)
        {
            _index = index;
            PseudoCodeLineNumber = pseudoCodeLineNumber;
        }

        public void Execute(IList<SortItem> items) => items[_index].IsSelected = true;

        public void Highlight(IList<SortItem> items)
        {
            items[_index].IsSelected = true;   
        }

        public void Undo(IList<SortItem> items) => items[_index].IsSelected = false;
    }
}
