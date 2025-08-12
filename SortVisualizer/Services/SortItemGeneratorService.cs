using SortVisualizer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SortVisualizer.Services
{
    public class SortItemGeneratorService
    {
        private readonly Random _random = new();

        public ObservableCollection<SortItem> GenerateRandom(int maxElementsValue = 50)
        {
            var items = new ObservableCollection<SortItem>();
            for (int i = 0; i < maxElementsValue; i++)
                items.Add(new SortItem { Value = _random.Next(10, 500) });
            return items;
        }

        public ObservableCollection<SortItem> GenerateReversed(int maxElementsValue = 50)
        {
            var items = new ObservableCollection<SortItem>();
            for (int i = 0; i < maxElementsValue; i++)
                items.Add(new SortItem { Value = 500 - i * 5 });
            return items;
        }

        public ObservableCollection<SortItem> GenerateNearlySorted(int maxElementsValue = 50)
        {
            var items = new ObservableCollection<SortItem>(
                Enumerable.Range(0, maxElementsValue)
                          .Select(i => new SortItem { Value = 10 + i * 10 })
            );

            int swapCount = _random.Next(1, 4);
            for (int s = 0; s < swapCount; s++)
            {
                int index = _random.Next(0, items.Count - 2);
                (items[index], items[index + 1]) = (items[index + 1], items[index]);
            }

            return items;
        }
        public ObservableCollection<SortItem> GenerateManyDuplicates(int maxElementsValue = 50)
        {
            var items = new ObservableCollection<SortItem>();

            int[] possibleValues = Enumerable.Range(0, 8)
                                             .Select(i => _random.Next(10, 500))
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
