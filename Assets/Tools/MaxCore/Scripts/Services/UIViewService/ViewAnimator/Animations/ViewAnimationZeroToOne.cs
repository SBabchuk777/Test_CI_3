using System;
using DG.Tweening;
using Tools.MaxCore.Scripts.ComponentHelp;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService.ViewAnimator.Animations
{
    public class ViewAnimationZeroToOne : BaseViewAnimator
    {
        [SerializeField] private ImageFader _fader;
        [SerializeField] private Transform _view;

        [SerializeField] private Ease _easeType;

        public override void Open(Action callback)
        {
            _view.localScale = Vector3.zero;

            _fader.FadeTo(0.3f, () =>
            {
                _view.DOScale(Vector3.one, .3f)
                    .OnComplete(() => callback?.Invoke())
                    .Play();
            });
        }

        public override void Close()
        {
            _view.DOScale(Vector3.zero, .3f)
                .SetEase(_easeType)
                .OnComplete(() => { _fader.FadeOut(0.3f, () => BaseView.DestroyView()); })
                .Play();
        }
    }
}