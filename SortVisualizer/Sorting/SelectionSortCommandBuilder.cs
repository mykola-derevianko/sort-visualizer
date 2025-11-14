using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SortVisualizer.Sorting
{
    public class SelectionSortCommandBuilder : ISortCommandBuilder
    {
        public SortAlgorithm Algorithm => SortAlgorithm.SelectionSort;
        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var commands = new List<ISortCommand>();
            var simulatedValues = items.Select(i => i.Value).ToList();

            for (int i = 0; i < simulatedValues.Count - 1; i++)
            {
                int minIndex = i;
                
                commands.Add(new SelectCommand(i, 1));

                for (int j = i + 1; j < simulatedValues.Count; j++)
                {
                    commands.Add(new CompareCommand(j, minIndex, 3));
                    
                    if (simulatedValues[j] < simulatedValues[minIndex])
                    {
                        minIndex = j;
                        commands.Add(new SelectCommand(j, 4));
                    }
                }

                if (minIndex != i)
                {
                    commands.Add(new SwapCommand(i, minIndex, 6));
                    (simulatedValues[i], simulatedValues[minIndex]) = (simulatedValues[minIndex], simulatedValues[i]);
                }
            }

            return commands;
        }
    }
}