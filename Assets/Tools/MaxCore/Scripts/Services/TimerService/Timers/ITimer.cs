using System;
using System.Threading;

namespace Tools.MaxCore.Scripts.Services.TimerService.Timers
{
    public interface ITimer
    {
        float CurrentSeconds { get; }

        void Start(CancellationToken cancellationToken);
        void Resume();
        void Pause();
        void Stop();
        void Reset();
        void SetSeconds(float time);
        event Action<float> OnSecondPassed;
        event Action OnTimerFinished;
    }
}