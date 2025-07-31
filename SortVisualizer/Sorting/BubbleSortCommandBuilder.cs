using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Sorting
{
    public class BubbleSortCommandBuilder
    {
        public List<ISortCommand> Build(IList<SortItem> items)
        {
            var commands = new List<ISortCommand>();

            var simulatedValues = items.Select(i => i.Value).ToList();

            for (int i = 0; i < simulatedValues.Count - 1; i++)
            {
                bool isSorted = true;

                for (int j = 0; j < simulatedValues.Count - i - 1; j++)
                {
                    commands.Add(new CompareCommand(j, j + 1));

                    if (simulatedValues[j] > simulatedValues[j + 1])
                    {
                        commands.Add(new SwapCommand(j, j + 1));
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
