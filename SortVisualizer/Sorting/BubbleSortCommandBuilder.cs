using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SortVisualizer.Sorting
{
    public class BubbleSortCommandBuilder : ISortCommandBuilder
    {
        public SortAlgorithm Algorithm => SortAlgorithm.BubbleSort;
        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var commands = new List<ISortCommand>();
            var simulatedValues = items.Select(i => i.Value).ToList();

            for (int i = 0; i < simulatedValues.Count - 1; i++)
            {
                bool isSorted = true;

                for (int j = 0; j < simulatedValues.Count - i - 1; j++)
                {
                    commands.Add(new CompareCommand(j, j + 1, 3));

                    if (simulatedValues[j] > simulatedValues[j + 1])
                    {
                        commands.Add(new SwapCommand(j, j + 1, 4));
                        isSorted = false;

                        (simulatedValues[j], simulatedValues[j + 1]) = (simulatedValues[j + 1], simulatedValues[j]);
                    }
                }

                if (isSorted)
                    break;
            }

            return commands;
        }
    }
}