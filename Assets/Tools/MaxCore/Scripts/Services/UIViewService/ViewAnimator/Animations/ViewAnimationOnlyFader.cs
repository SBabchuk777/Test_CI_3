using System;
using Tools.MaxCore.Scripts.ComponentHelp;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService.ViewAnimator.Animations
{
    public class ViewAnimationOnlyFader : BaseViewAnimator
    {
        [SerializeField] private ImageFader _fader;
        [SerializeField] private Transform _view;
        
        public override void Open(Action callback)
        {
            _view.gameObject.SetActive(false);
            _fader.FadeTo(0.3f, ()=>
            {
                _view.gameObject.SetActive(true);
                callback?.Invoke();
            });
        }

        public override void Close()
        {
            _view.gameObject.SetActive(false);
            _fader.FadeOut(0.3f, () => BaseView.DestroyView());
        }
    }
}