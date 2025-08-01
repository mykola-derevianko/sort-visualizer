using SortVisualizer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SortVisualizer.Services
{
    public class SortItemGeneratorService : ISortItemGenerator
    {
        private readonly Random _random = new();

        public ObservableCollection<SortItem> GenerateRandom()
        {
            var items = new ObservableCollection<SortItem>();
            for (int i = 0; i < 50; i++)
                items.Add(new SortItem { Value = _random.Next(10, 300) });
            return items;
        }

        public ObservableCollection<SortItem> GenerateReversed()
        {
            var items = new ObservableCollection<SortItem>();
            for (int i = 0; i < 50; i++)
                items.Add(new SortItem { Value = 300 - i * 5 });
            return items;
        }

        public ObservableCollection<SortItem> GenerateNearlySorted()
        {
            var items = GenerateRandom();
            items = new ObservableCollection<SortItem>(items.OrderBy(i => i.Value).ToList());
            if (items.Count > 5)
            {
                (items[10], items[11]) = (items[11], items[10]); // slight disorder
            }
            return items;
        }
    }

}
