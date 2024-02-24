using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.TimerService.Timers
{
    public class TimerCountdown : ITimer
    {
        private bool isStarted;
        private bool isExecuting;

        public event Action<float> OnSecondPassed;

        public event Action OnTimerFinished;

        public float CurrentSeconds { get; private set; }
        
        public void SetSeconds(float time)
        {
            CurrentSeconds = time;
        }

        public async void Start(CancellationToken cancellationToken)
        {
            if (isStarted)
                return;

            isStarted = true;
            isExecuting = true;
            
            await UpdateCountdownTimer(cancellationToken);
        }

        public void Resume() =>
            isExecuting = true;

        public void Pause() =>
            isExecuting = false;

        public void Reset()
        {
            CurrentSeconds = 0f;
            isStarted = false;
            isExecuting = false;

            OnTimerFinished = null;
            OnSecondPassed = null;
        }

        public void Stop()
        {
            Reset();
        }
        private async UniTask UpdateCountdownTimer(CancellationToken cancellationToken)
        {
            float previousSecond = Mathf.Floor(CurrentSeconds);

            await UniTask.WaitWhile(() =>
            {
                if (isExecuting)
                {
                    CurrentSeconds -= Time.deltaTime;
                    float currentSecond = Mathf.Floor(CurrentSeconds);

                    if (currentSecond < previousSecond)
                    {
                        previousSecond = currentSecond;
                        OnSecondPassed?.Invoke(CurrentSeconds <= 0 ? 0f : CurrentSeconds);
                    }
                    
                    if (CurrentSeconds <= 0f)
                    {
                        OnTimerFinished?.Invoke();
                        Reset();
                    }
                }

                return isStarted && !cancellationToken.IsCancellationRequested;
            }, cancellationToken: cancellationToken);
        }
    }
}