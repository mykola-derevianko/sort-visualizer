using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class SwapCommand : ISortCommand
    {
        private readonly int _indexA;
        private readonly int _indexB;
        
        public int PseudoCodeLineNumber { get; }

        public SwapCommand(int indexA, int indexB, int pseudoCodeLineNumber = -1)
        {
            _indexA = indexA;
            _indexB = indexB;
            PseudoCodeLineNumber = pseudoCodeLineNumber;
        }

        public void Execute(IList<SortItem> items)
        {
            items[_indexA].IsSwapping = true;
            items[_indexB].IsSwapping = true;

            var temp = items[_indexA].Value;
            items[_indexA].Value = items[_indexB].Value;
            items[_indexB].Value = temp;
        }

        public void Highlight(IList<SortItem> items)
        {
            items[_indexA].IsSwapping = true;
            items[_indexB].IsSwapping = true;
        }

        public void Undo(IList<SortItem> items)
        {
            var temp = items[_indexA].Value;
            items[_indexA].Value = items[_indexB].Value;
            items[_indexB].Value = temp;

            items[_indexA].IsSwapping = false;
            items[_indexB].IsSwapping = false;
        }
    }
}
