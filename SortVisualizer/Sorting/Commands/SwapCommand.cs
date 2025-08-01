using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class SwapCommand : ISortCommand
    {
        private readonly int _indexA;
        private readonly int _indexB;

        public SwapCommand(int indexA, int indexB)
        {
            _indexA = indexA;
            _indexB = indexB;
        }

        public void Execute(IList<SortItem> items)
        {
            items[_indexA].IsSwapping = true;
            items[_indexB].IsSwapping = true;
            (items[_indexA], items[_indexB]) = (items[_indexB], items[_indexA]);
        }
        public void Highlight(IList<SortItem> items)
        {
            items[_indexA].IsSwapping = true;
            items[_indexB].IsSwapping = true;
        }

        public void Undo(IList<SortItem> items)
        {
            Execute(items);
        }
    }

}
