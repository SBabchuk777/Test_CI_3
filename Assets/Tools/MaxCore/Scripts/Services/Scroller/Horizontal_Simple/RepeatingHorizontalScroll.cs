using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Tools.MaxCore.Scripts.Services.Scroller.Horizontal_Simple.Modules;
using Tools.MaxCore.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.Services.Scroller.Horizontal_Simple
{
    public class RepeatingHorizontalScroll : MonoBehaviour
    {
        [SerializeField] private SwipeTouchInput _touchInput;
        [SerializeField] private List<RectTransform> _elements;
        [SerializeField] private List<Image> _points;
        [SerializeField] private int _sizeDeltaX;

        public int CurrentIndex { get; private set; }
        private Sequence moveSequence;
        private bool isScroll;

        public event Action<int> OnFinishScroll;

        public void Start()
        {
            _touchInput.OnSwipe += Swipe;

            SetElements();
        }

        private void SetElements()
        {
            var x = 0f;
            for (var index = 0; index < _elements.Count - 1; index++)
            {
                var element = _elements[index];
                element.anchoredPosition = element.anchoredPosition.SetXPosition(x);
                x += _sizeDeltaX;
            }

            var lastElement = _elements[^1];
            lastElement.anchoredPosition = lastElement.anchoredPosition.SetXPosition(-_sizeDeltaX);

            _elements[0].localScale = new Vector2(1.5f, 1.5f);
        }

        private void Swipe(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Right:
                    MoveElementTo(CurrentIndex - 1);
                    break;
                case SwipeDirection.Left:
                    MoveElementTo(CurrentIndex + 1);
                    break;
            }
        }

        private void MoveElementTo(int index)
        {
            if (isScroll)
            {
                return;
            }

            isScroll = true;
            moveSequence = DOTween.Sequence();

            var direction = CurrentIndex > index ? _sizeDeltaX : -_sizeDeltaX;
            var sortedForX = _elements.OrderBy(e => e.anchoredPosition.x).ToList();
            
            foreach (var element in _elements)
            {
                if (element.anchoredPosition.x + direction > _sizeDeltaX * 2)
                {
                    element.anchoredPosition = new Vector2( Mathf.RoundToInt(sortedForX.First().anchoredPosition.x -_sizeDeltaX),element.anchoredPosition.y);
                }

                if (element.anchoredPosition.x + direction < -_sizeDeltaX * 2)
                {
                    element.anchoredPosition = new Vector2( Mathf.RoundToInt(sortedForX.Last().anchoredPosition.x + _sizeDeltaX) ,element.anchoredPosition.y);
                }

                var roundPosition = Mathf.RoundToInt(element.anchoredPosition.x + direction);
                moveSequence.Join(element.DOAnchorPosX(roundPosition, 0.25f));
                moveSequence.Join(element.DOScale(roundPosition == 0 ? new Vector2(1.5f,1.5f): Vector2.one, 0.25f));
            }

            index = SetIndex(index);
            SetPaginator(index);

            moveSequence
                .OnComplete(() =>
                {
                    OnFinishScroll?.Invoke(index);
                    isScroll = false;
                })
                .Play();
        }

        private int SetIndex(int index)
        {
            if (index < 0)
            {
                index = _elements.Count - 1;
            }

            if (index > _elements.Count - 1)
            {
                index = 0;
            }

            CurrentIndex = index;
            return index;
        }

        private void SetPaginator(int index)
        {
            for (int i = 0; i < _points.Count; i++)
            {
                moveSequence.Join(_points[i].DOFade(i == index ? 1f : 0f, 0.25f));
            }
        }
    }
}