using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class CompareCommand : ISortCommand
    {
        private readonly int _indexA;
        private readonly int _indexB;

        public CompareCommand(int indexA, int indexB)
        {
            _indexA = indexA;
            _indexB = indexB;
        }

        public void Execute(IList<SortItem> items)
        {
            items[_indexA].IsComparing = true;
            items[_indexB].IsComparing = true;
        }

        public void Undo(IList<SortItem> items)
        {
            items[_indexA].IsComparing = false;
            items[_indexB].IsComparing = false;
        }
    }
}
