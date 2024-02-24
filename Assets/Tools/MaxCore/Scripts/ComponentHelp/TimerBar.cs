using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.ComponentHelp
{
    public class TimerBar : MonoBehaviour
    {
        private Slider Bar => GetComponent<Slider>();

        private Tween runBarTween;

        private void OnDestroy() => Kill();

        public void SetValue(float value)
        {
            if (value is >= 0 and <= 1)
                Bar.value = value;
        }

        public void RunBarOut(float time, Action callback = null)
        {
            runBarTween = DOTween.To(() => 1f, x => Bar.value = x, 0f, time)
                .SetEase(Ease.Linear)
                .OnComplete(()=> callback?.Invoke())
                .Play();
        }

        public void RunBarTo(float time, Action callback = null)
        {
            runBarTween = DOTween.To(() => 0f, x => Bar.value = x, 1f, time)
                .SetEase(Ease.Linear)
                .OnComplete(()=> callback?.Invoke())
                .Play();
        }

        public void Kill() => 
            runBarTween?.Kill();
    }
}