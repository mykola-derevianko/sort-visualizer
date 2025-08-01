using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.Services;
public interface ISortPlaybackService
{
    event Action<int>? OnStepChanged;
    event Action<bool>? OnPlayingChanged;

    int CurrentStep { get; }
    int TotalSteps { get; }
    bool IsPlaying { get; }

    Task PlayAsync(IList<SortItem> items, double speedMultiplier, CancellationToken token);
    void Pause();
    void Cancel();
    void StepForward(IList<SortItem> items);
    void StepBackward(IList<SortItem> items);
    void JumpToStep(int stepIndex, IList<SortItem> items);
}
