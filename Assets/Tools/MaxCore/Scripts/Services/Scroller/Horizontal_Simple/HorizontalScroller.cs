using System;
using System.Collections.Generic;
using DG.Tweening;
using Tools.MaxCore.Scripts.Services.Scroller.Horizontal_Simple.Modules;
using Tools.MaxCore.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.Services.Scroller.Horizontal_Simple
{
    public class HorizontalScroller : MonoBehaviour
    {
        [SerializeField] private SwipeTouchInput _touchInput;
        [SerializeField] private List<RectTransform> _elements;
        [SerializeField] private List<Image> _points;
        [SerializeField] private float _sizeDeltaX;

        public int CurrentIndex { get; private set; }
        private Sequence moveSequence;

        public event Action<int> OnFinishScroll;

        public void Start()
        {
            _touchInput.OnSwipe += Swipe;

            SetPositionElements();
        }

        private void SetPositionElements()
        {
            var x = 0f;
            foreach (var element in _elements)
            {
                element.anchoredPosition = element.anchoredPosition.SetXPosition(x);
                x += _sizeDeltaX;
            }
        }

        private void Swipe(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Right when CurrentIndex > 0:
                    MoveElementTo(CurrentIndex - 1);
                    break;
                case SwipeDirection.Left when CurrentIndex < _elements.Count - 1:
                    MoveElementTo(CurrentIndex + 1);
                    break;
            }
        }

        private void MoveElementTo(int index)
        {
            moveSequence?.Kill();
            moveSequence = DOTween.Sequence();

            for (int i = 0; i < _points.Count; i++)
            {
                moveSequence.Join(_points[i].DOFade(i == index ? 1f : 0f, 0.25f));
            }

            var direction = CurrentIndex > index ? _sizeDeltaX : -_sizeDeltaX;
            foreach (var element in _elements)
            {
                moveSequence.Join(element.DOAnchorPosX(element.anchoredPosition.x + direction, 0.25f));
            }

            moveSequence
                .OnComplete(() => OnFinishScroll?.Invoke(index))
                .Play();
            
            CurrentIndex = index;
        }
    }
}