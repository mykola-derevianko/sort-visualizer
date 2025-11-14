using SortVisualizer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SortVisualizer.Services
{
    public class SortItemGeneratorService
    {
        private readonly Random _random = new();

        private const int DefaultMaxElements = 50;
        private const int MinRandomValue = 1;
        private const int MaxRandomValue = 50;
        private const int MinSwapCount = 1;
        private const int MaxSwapCount = 4;
        private const int DuplicateValuesCount = 8;
        private const int MinDuplicateValue = 10;
        private const int MaxDuplicateValue = 50;

        public ObservableCollection<SortItem> GenerateRandom(int maxElementsValue = DefaultMaxElements)
        {
            var items = new ObservableCollection<SortItem>();
            for (int i = 0; i < maxElementsValue; i++)
                items.Add(new SortItem { Value = _random.Next(MinRandomValue, MaxRandomValue) });
            return items;
        }

        public ObservableCollection<SortItem> GenerateReversed(int maxElementsValue = DefaultMaxElements)
        {
            var items = new ObservableCollection<SortItem>();

            for (int i = maxElementsValue; i >= 1; i--)
            {
                items.Add(new SortItem { Value = i });
            }

            return items;
        }

        public ObservableCollection<SortItem> GenerateNearlySorted(int maxElementsValue = DefaultMaxElements)
        {
            var items = new ObservableCollection<SortItem>(
                Enumerable.Range(1, maxElementsValue)
                          .Select(x => new SortItem { Value = x })
            );

            int swapCount = _random.Next(MinSwapCount, MaxSwapCount);

            for (int s = 0; s < swapCount; s++)
            {
                int index = _random.Next(0, items.Count - 1);
                (items[index], items[index + 1]) = (items[index + 1], items[index]);
            }

            return items;
        }


        public ObservableCollection<SortItem> GenerateManyDuplicates(int maxElementsValue = DefaultMaxElements)
        {
            var items = new ObservableCollection<SortItem>();

            int[] possibleValues = Enumerable.Range(0, DuplicateValuesCount)
                                             .Select(i => _random.Next(MinDuplicateValue, MaxDuplicateValue))
                                             .ToArray();

            for (int i = 0; i < maxElementsValue; i++)
            {
                int value = possibleValues[_random.Next(possibleValues.Length)];
                items.Add(new SortItem { Value = value });
            }

            return items;
        }
    }
}
