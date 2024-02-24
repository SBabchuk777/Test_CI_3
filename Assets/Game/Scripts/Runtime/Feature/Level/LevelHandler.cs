using System;
using Game.Scripts.Runtime.Feature.Level.Common;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private FinishDetector _finishDetector;

        public event Action OnLose;
        public bool IsLose;

        private void Start()
        {
            _finishDetector.OnTouch += NotifyLose;
        }

        private void NotifyLose()
        {
            if (!IsLose)
            {
                OnLose?.Invoke();
                IsLose = true;
            }
            
        }

        public void Reset()
        {
            IsLose = false;
        }
    }
}