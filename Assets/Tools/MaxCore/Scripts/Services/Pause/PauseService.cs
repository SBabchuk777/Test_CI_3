using System.Collections.Generic;
using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.SateMachine;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.Pause
{
    public class PauseService : MonoBehaviour
    {
        private readonly List<IPauseListener> PauseListeners = new List<IPauseListener>();
        public bool IsPaused { get; set; }

        [Inject] private GameStateMachine stateMachine;
        private LevelState lastState;

        public void StartPause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                foreach (var listener in PauseListeners)
                {
                    listener.OnStartPause();
                }

                lastState = stateMachine.LevelState;
                stateMachine.SetLevelState(LevelState.Pause);
            }
        }

        public void FinishPause()
        {
            if (IsPaused)
            {
                IsPaused = false;
                
                foreach (var listener in PauseListeners)
                {
                    listener.OnFinishPause();
                }
                
                stateMachine.SetLevelState(lastState);
            }
        }

        public void RegisterListener(IPauseListener listener)
        {
            if (!PauseListeners.Contains(listener))
            {
                PauseListeners.Add(listener);
            }
        }

        public void UnregisterListener(IPauseListener listener)
        {
            PauseListeners.Remove(listener);
        }
    }
}