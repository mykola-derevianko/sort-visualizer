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

                commands.Add(new SelectCommand(i));

                while (j >= 0 && simulatedValues[j] > key)
                {
                    commands.Add(new CompareCommand(j, i));
                    j--;
                }

                if (j + 1 != i)
                {
                    commands.Add(new MoveCommand(i, j + 1, 6));
                    simulatedValues.RemoveAt(i);
                    simulatedValues.Insert(j + 1, key);
                }
            }

            return commands;
        }
    }
}
