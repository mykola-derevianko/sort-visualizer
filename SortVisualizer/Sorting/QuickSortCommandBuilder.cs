using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;

namespace SortVisualizer.Sorting
{
    public class QuickSortCommandBuilder : ISortCommandBuilder
    {
        public SortAlgorithm Algorithm => SortAlgorithm.QuickSort;
        private List<ISortCommand> _commands = new();

        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var simulatedValues = new List<int>();
            foreach (var item in items)
                simulatedValues.Add(item.Value);

            _commands.Clear();
            QuickSort(simulatedValues, 0, simulatedValues.Count - 1);
            return _commands;
        }

        private void QuickSort(List<int> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);

                QuickSort(arr, low, pi - 1);

                QuickSort(arr, pi + 1, high);
            }
        }

        private int Partition(List<int> arr, int low, int high)
        {
            int pivot = arr[high];
            _commands.Add(new SetPivotCommand(high, 5));

            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                _commands.Add(new CompareCommand(j, high, 8));

                if (arr[j] < pivot)
                {
                    i++;

                    if (i != j)
                    {
                        _commands.Add(new CompareCommand(i, j, 10));

                        _commands.Add(new SwapCommand(i, j, 10));
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }
            }

            if (i + 1 != high)
            {
                _commands.Add(new SwapCommand(i + 1, high, 11));
                (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            }

            return i + 1;
        }
    }
}
