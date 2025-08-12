using SortVisualizer.Models;
using SortVisualizer.Services;
using SortVisualizer.Sorting;
using SortVisualizer.Sorting.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.Services;

public class SortPlaybackService
{
    private readonly CommandPlayer _player;
    private CancellationTokenSource? _cts;
    private bool _isPlaying;

    public event Action<int>? OnStepChanged;
    public event Action<bool>? OnPlayingChanged;

    public int CurrentStep => _player.CurrentStep;
    public int TotalSteps => _player.TotalSteps;
    public bool IsPlaying => _isPlaying;

    public SortPlaybackService(IEnumerable<ISortCommand> commands)
    {
        _player = new CommandPlayer(commands, step => OnStepChanged?.Invoke(step));
    }

    public async Task PlayAsync(IList<SortItem> items, double speedMultiplier, CancellationToken token)
    {
        if (_isPlaying) return;

        _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        _isPlaying = true;
        OnPlayingChanged?.Invoke(true);

        try
        {
            await _player.PlayAsync(items, _cts.Token, speedMultiplier);
        }
        catch (OperationCanceledException){}
        finally
        {
            _isPlaying = false;
            OnPlayingChanged?.Invoke(false);
            OnStepChanged?.Invoke(_player.CurrentStep);
            _cts = null;
        }
    }

    public void Pause()
    {
        _player.Pause();
        _cts?.Cancel();
        _isPlaying = false;
        OnPlayingChanged?.Invoke(false);
    }

    public void Cancel()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;

        _player.Pause();
        _isPlaying = false;
        OnPlayingChanged?.Invoke(false);
    }

    public void StepForward(IList<SortItem> items)
    {
        _player.StepForward(items);
        OnStepChanged?.Invoke(_player.CurrentStep);
    }

    public void StepBackward(IList<SortItem> items)
    {
        _player.StepBackward(items);
        OnStepChanged?.Invoke(_player.CurrentStep);
    }

    public void JumpToStep(int stepIndex, IList<SortItem> items)
    {
        _player.JumpToStep(stepIndex, items);
        OnStepChanged?.Invoke(_player.CurrentStep);
    }
}
