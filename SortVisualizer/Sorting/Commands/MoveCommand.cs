using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Sorting.Commands
{
    public class MoveCommand : ISortCommand
    {
        private readonly int _fromIndex;
        private readonly int _toIndex;

        public MoveCommand(int fromIndex, int toIndex)
        {
            _fromIndex = fromIndex;
            _toIndex = toIndex;
        }

        public void Execute(IList<SortItem> items)
        {
            if (_fromIndex == _toIndex) return;

            var item = items[_fromIndex];
            items.RemoveAt(_fromIndex);
            items.Insert(_toIndex, item);

            item.IsSwapping = true;
        }

        public void Undo(IList<SortItem> items)
        {
            if (_fromIndex == _toIndex) return;

            var item = items[_toIndex];
            items.RemoveAt(_toIndex);
            items.Insert(_fromIndex, item);

            item.IsSwapping = false;
        }
    }
}
