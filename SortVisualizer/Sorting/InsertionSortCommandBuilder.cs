using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SortVisualizer.Sorting
{
    public class InsertionSortCommandBuilder
    {
        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var commands = new List<ISortCommand>();
            var simulatedValues = items.Select(i => i.Value).ToList();

            for (int i = 1; i < simulatedValues.Count; i++)
            {
                int key = simulatedValues[i];
                int j = i - 1;

                // Highlight picked element
                commands.Add(new SelectCommand(i));

                // Shift comparison until correct position
                while (j >= 0 && simulatedValues[j] > key)
                {
                    commands.Add(new CompareCommand(j, i));
                    j--;
                }

                // Insert into correct position (only if moved)
                if (j + 1 != i)
                {
                    commands.Add(new MoveCommand(i, j + 1));
                    simulatedValues.RemoveAt(i);
                    simulatedValues.Insert(j + 1, key);
                }
            }

            return commands;
        }
    }
}
