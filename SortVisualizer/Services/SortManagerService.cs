using SortVisualizer.Models;
using SortVisualizer.Sorting;
using System;
using System.Collections.ObjectModel;

namespace SortVisualizer.Services;

public class SortManagerService : ISortManagerService
{
    private readonly ISortItemGenerator _generator;
    private readonly BubbleSortCommandBuilder _bubbleBuilder = new();
    private readonly QuickSortCommandBuilder _quickBuilder = new();

    public SortManagerService(ISortItemGenerator generator)
    {
        _generator = generator;
    }

    public ObservableCollection<SortItem> GenerateItems(GenerateType type) => type switch
    {
        GenerateType.Random => _generator.GenerateRandom(),
        GenerateType.Reversed => _generator.GenerateReversed(),
        GenerateType.NearlySorted => _generator.GenerateNearlySorted(),
        _ => _generator.GenerateRandom(),
    };

    public CommandPlayer StartSort(ObservableCollection<SortItem> items, SortAlgorithm algorithm, Action<int>? onStepChanged)
    {
        var commands = algorithm switch
        {
            SortAlgorithm.BubbleSort => _bubbleBuilder.Build(items),
            SortAlgorithm.QuickSort => _quickBuilder.Build(items),
            _ => throw new NotSupportedException("Unsupported algorithm")
        };

        return new CommandPlayer(commands, onStepChanged);
    }
}
