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
        private const int MaxRandomValue = 60;
        private const int ReversedStartValue = 60;
        private const int ReversedStepValue = 5;
        private const int NearlySortedStartValue = 10;
        private const int NearlySortedStepValue = 10;
        private const int MinSwapCount = 1;
        private const int MaxSwapCount = 4;
        private const int DuplicateValuesCount = 8;
        private const int MinDuplicateValue = 10;
        private const int MaxDuplicateValue = 60;

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
            for (int i = 0; i < maxElementsValue; i++)
                items.Add(new SortItem { Value = ReversedStartValue - i * ReversedStepValue });
            return items;
        }

        public ObservableCollection<SortItem> GenerateNearlySorted(int maxElementsValue = DefaultMaxElements)
        {
            var items = new ObservableCollection<SortItem>(
                Enumerable.Range(0, maxElementsValue)
                          .Select(i => new SortItem { Value = NearlySortedStartValue + i * NearlySortedStepValue })
            );

            int swapCount = _random.Next(MinSwapCount, MaxSwapCount);
            for (int s = 0; s < swapCount; s++)
            {
                int index = _random.Next(0, items.Count - 2);
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
