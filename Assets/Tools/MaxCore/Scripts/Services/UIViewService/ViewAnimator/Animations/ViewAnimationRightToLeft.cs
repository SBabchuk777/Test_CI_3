using System;
using DG.Tweening;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService.ViewAnimator.Animations
{
    public class ViewAnimationRightToLeft : BaseViewAnimator
    {
        [SerializeField] private Ease _easeType;

        private float CameraWidth => Camera.main.aspect * Camera.main.orthographicSize * 2f;
       
        public override void Open(Action callback)
        {
            transform.position = new Vector3(CameraWidth, transform.position.y, transform.position.z);
            
            transform
                .DOLocalMoveX(0, 0.5f)
                .SetEase(_easeType)
                .OnComplete(() => callback?.Invoke())
                .Play();
        }

        public override void Close()
        {
            transform
                .DOMoveX(-CameraWidth, 0.5f)
                .SetEase(_easeType)
                .OnComplete(() => BaseView.DestroyView())
                .Play();
        }
    }
}