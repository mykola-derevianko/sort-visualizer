using SortVisualizer.Models;
using SortVisualizer.Sorting.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.Sorting
{
    public class CommandPlayer
    {
        private readonly IList<ISortCommand> _commands;
        private int _currentIndex = -1;
        private bool _isPlaying = false;

        public int CurrentStep => _currentIndex + 1;
        public int TotalSteps => _commands.Count;

        public CommandPlayer(IEnumerable<ISortCommand> commands)
        {
            _commands = commands.ToList();
        }

        public void StepForward(IList<SortItem> items)
        {
            if (_currentIndex + 1 < _commands.Count)
            {
                ClearAllHighlights(items);
                _commands[++_currentIndex].Execute(items);
            }
        }

        public void StepBackward(IList<SortItem> items)
        {
            if (_currentIndex >= 0)
            {
                _commands[_currentIndex].Undo(items);
                _currentIndex--;

                ClearAllHighlights(items);

                if (_currentIndex >= 0)
                {
                    if (_commands[_currentIndex] is CompareCommand)
                        _commands[_currentIndex].Execute(items);
                }
            }
        }

        private void ClearAllHighlights(IList<SortItem> items)
        {
            foreach (var item in items)
            {
                item.IsComparing = false;
            }
        }


        public async Task PlayAsync(IList<SortItem> items, CancellationToken token, double speedMultiplier)
        {
            _isPlaying = true;

            while (_isPlaying && _currentIndex + 1 < _commands.Count)
            {
                token.ThrowIfCancellationRequested();

                StepForward(items);

                await Task.Delay(TimeSpan.FromMilliseconds(200 / speedMultiplier), token);
            }

            _isPlaying = false;
        }

        public void Pause() => _isPlaying = false;

        public void JumpToStep(int stepIndex, IList<SortItem> items)
        {
            while (_currentIndex < stepIndex - 1)
                StepForward(items);
            while (_currentIndex >= stepIndex)
                StepBackward(items);
        }
    }

}
