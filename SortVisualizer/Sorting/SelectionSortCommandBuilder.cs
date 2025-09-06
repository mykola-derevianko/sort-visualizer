using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SortVisualizer.Sorting
{
    public class SelectionSortCommandBuilder
    {
        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var commands = new List<ISortCommand>();
            var simulatedValues = items.Select(i => i.Value).ToList();

            for (int i = 0; i < simulatedValues.Count - 1; i++)
            {
                int minIndex = i;
                
                // Select the current position
                commands.Add(new SelectCommand(i));

                // Find the minimum element in the remaining unsorted array
                for (int j = i + 1; j < simulatedValues.Count; j++)
                {
                    commands.Add(new CompareCommand(j, minIndex));
                    
                    if (simulatedValues[j] < simulatedValues[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // Swap the found minimum element with the first element
                if (minIndex != i)
                {
                    commands.Add(new SwapCommand(i, minIndex));
                    (simulatedValues[i], simulatedValues[minIndex]) = (simulatedValues[minIndex], simulatedValues[i]);
                }
            }

            return commands;
        }
    }
}