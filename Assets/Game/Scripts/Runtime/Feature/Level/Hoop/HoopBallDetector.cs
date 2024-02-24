using System;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Hoop
{
    public class HoopBallDetector : MonoBehaviour
    {
        public event Action OnBallInArea;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<BallHandler>())
            {
                OnBallInArea?.Invoke();
            }
        }

        public void DeactivateCollider() =>
            gameObject.SetActive(false);

        public void ActivateCollider() =>
            gameObject.SetActive(true);
    }
}