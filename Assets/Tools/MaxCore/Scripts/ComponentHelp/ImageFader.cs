using System;
using DG.Tweening;
using Tools.MaxCore.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.ComponentHelp
{
    [RequireComponent(typeof(Image))]
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private float FadeEndValue;
        private Image Image => GetComponent<Image>();
        
        public void FadeTo(float time, Action callback = null)
        {
            TurnOn();
            Image.SetAlpha(0);
            Fade(time, FadeEndValue, callback);
        }

        public void FadeOut(float time, Action callback = null)
        {
            TurnOn();

            Fade(time, 0f, () =>
            {
                TurnOff();
                callback?.Invoke();
            });
        }

        public void FadeOutAwait(float timeAwait, float time, Action callback = null, Action callbackAfterAwait = null)
        {
            TurnOn();
            Image.color = new Color(0, 0, 0, 1);

            DOTween.Sequence()
                .Append(DOVirtual.DelayedCall(timeAwait, () => {callbackAfterAwait?.Invoke(); }))
                .Append(DOVirtual.DelayedCall(0, () =>
                {
                    Fade(time, 0f, () =>
                    {
                        TurnOff();
                        callback?.Invoke();
                    });
                }))
                .Play();
        }

        private void TurnOff() =>
            gameObject.SetActive(false);

        private void TurnOn() => 
            gameObject.SetActive(true);

        private void Fade(float time, float endValue, Action callback) =>
            DOTween.Sequence()
                .Append(Image.DOFade(endValue, time))
                .AppendCallback(() => callback?.Invoke())
                .Play();
    }
}