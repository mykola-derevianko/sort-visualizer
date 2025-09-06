using SortVisualizer.Models;
using SortVisualizer.Sorting;
using System;
using System.Collections.ObjectModel;

namespace SortVisualizer.Services;

public class SortManagerService
{
    private readonly SortItemGeneratorService _generator;
    private readonly BubbleSortCommandBuilder _bubbleBuilder = new();
    private readonly QuickSortCommandBuilder _quickBuilder = new();
    private readonly InsertionSortCommandBuilder _insertionBuilder = new();
    private readonly SelectionSortCommandBuilder _selectionBuilder = new();

    public SortManagerService(SortItemGeneratorService generator)
    {
        _generator = generator;
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
        var commands = algorithm switch
        {
            SortAlgorithm.BubbleSort => _bubbleBuilder.Build(items),
            SortAlgorithm.QuickSort => _quickBuilder.Build(items),
            SortAlgorithm.InsertionSort => _insertionBuilder.Build(items),
            SortAlgorithm.SelectionSort => _selectionBuilder.Build(items),
            _ => throw new NotSupportedException("Unsupported algorithm")
        };

        return new CommandPlayer(commands, onStepChanged);
    }
}