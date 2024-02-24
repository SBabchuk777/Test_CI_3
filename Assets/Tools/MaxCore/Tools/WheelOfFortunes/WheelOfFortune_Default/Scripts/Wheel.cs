using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools.MaxCore.Tools.WheelOfFortunes.WheelOfFortune_Default.Scripts
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private AnimationCurve spinCurve;
        [SerializeField] private float _spinDuration = 3;

        public event Action OnStop;
        
        public void SpinWheel()
        {
            var finishRotation = Random.Range(1, 10) * 36 + (360 * 2);
            var angles = transform.eulerAngles.z;

            DOVirtual.Float(0f, 1f, _spinDuration,
                    time => transform.localEulerAngles = new Vector3(0f, 0f, angles + (finishRotation * time)))
                .SetEase(spinCurve)
                .OnComplete(() => OnStop?.Invoke())
                .Play();
        }
    }
}