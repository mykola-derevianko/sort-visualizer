using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortVisualizer.Sorting
{
    public class SortCommandBuilderFactory
    {
        private readonly Dictionary<SortAlgorithm, ISortCommandBuilder> _builders;

        public SortCommandBuilderFactory(IEnumerable<ISortCommandBuilder> builders)
        {
            _builders = builders.ToDictionary(b => b.Algorithm, b => b);
        }

        public ISortCommandBuilder GetBuilder(SortAlgorithm algorithm)
        {
            if (_builders.TryGetValue(algorithm, out var builder))
                return builder;

            throw new NotSupportedException($"Algorithm {algorithm} is not supported");
        }
    }
}