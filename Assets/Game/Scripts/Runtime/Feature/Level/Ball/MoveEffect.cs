using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class MoveEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private Tween tweenDestroy;
        
        public void Stop()
        {
            _particleSystem.Pause();
            tweenDestroy?.Pause();
        }

        public void Run()
        {
            _particleSystem.Play();
            tweenDestroy?.Play();
        }

        public void Initialize(Action onDestroy)
        {
            tweenDestroy = DOVirtual.DelayedCall(2f, () =>
            {
                Destroy(gameObject);
                onDestroy?.Invoke();
            }).Play();
        }
    }
}