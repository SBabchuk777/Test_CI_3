using System;
using Game.Scripts.Runtime.Feature.Level.Hoop;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Common
{
    public class FinishDetector : MonoBehaviour
    {
        public event Action OnTouch;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            var hoop = col.GetComponent<HoopHandler>();
            if (hoop != null && !hoop.IsGoal)
            {
                OnTouch?.Invoke();
            }
            
            if (col.GetComponent<BallHandler>())
            {
                OnTouch?.Invoke();
            }
        }
    }
}