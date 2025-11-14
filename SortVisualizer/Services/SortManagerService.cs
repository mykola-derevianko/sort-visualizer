using SortVisualizer.Models;
using SortVisualizer.Sorting;
using System;
using System.Collections.ObjectModel;

namespace SortVisualizer.Services
{
    public class SortManagerService
    {
        private readonly SortItemGeneratorService _generator;
        private readonly SortCommandBuilderFactory _builderFactory;

        public SortManagerService(SortItemGeneratorService generator, SortCommandBuilderFactory builderFactory)
        {
            _generator = generator;
            _builderFactory = builderFactory;
        }

        public ObservableCollection<SortItem> GenerateItems(GenerateType type, int maxElementValue = 50) => type switch
        {
            GenerateType.Random => _generator.GenerateRandom(maxElementValue),
            GenerateType.Reversed => _generator.GenerateReversed(maxElementValue),
            GenerateType.NearlySorted => _generator.GenerateNearlySorted(maxElementValue),
            GenerateType.ManyDuplicates => _generator.GenerateManyDuplicates(maxElementValue),
            _ => _generator.GenerateRandom(maxElementValue),
        };

        public CommandPlayer StartSort(ObservableCollection<SortItem> items, SortAlgorithm algorithm, Action<int>? onStepChanged)
        {
            var builder = _builderFactory.GetBuilder(algorithm);
            var commands = builder.Build(items);
            return new CommandPlayer(commands, onStepChanged);
        }
    }
}