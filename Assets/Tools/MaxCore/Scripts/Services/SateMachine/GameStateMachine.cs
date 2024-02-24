using System;
using Game.Scripts.Runtime.Services.SateMachine;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.SateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        public LevelState LevelState { get; private set; }

        public event Action<StateInfo> OnChangeState;
        
        public void SetLevelState(LevelState selectState)
        {
            var previewState = LevelState;
            LevelState = selectState;
            
            OnChangeState?.Invoke(new StateInfo(previewState, LevelState));
            Debug.Log(selectState);
        }
    }
}