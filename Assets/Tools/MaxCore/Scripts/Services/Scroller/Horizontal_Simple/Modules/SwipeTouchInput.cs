using System;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.Scroller.Horizontal_Simple.Modules
{
    public class SwipeTouchInput : MonoBehaviour
    {
        [SerializeField] private RectTransform _swipeArea;

        public event Action<SwipeDirection> OnSwipe;

        private Vector2 lastPointPosition;
        private bool isGottenSwipe;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var position = RectPosition();
                if (_swipeArea.rect.Contains(new Vector2(position.x, position.y)))
                {
                    isGottenSwipe = true;
                    lastPointPosition = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isGottenSwipe)
                {
                    return;
                }

                Vector2 currentPosition = Input.mousePosition;

                float horizontalLength = Mathf.Abs(currentPosition.x - lastPointPosition.x);
                float verticalLength = Mathf.Abs(currentPosition.y - lastPointPosition.y);

                if (horizontalLength < 60 && verticalLength < 60)
                    return;

                if (horizontalLength > verticalLength)
                {
                    float move = currentPosition.x - lastPointPosition.x;

                    if (move > 0)
                        NotifySwipe(SwipeDirection.Right);
                    else
                        NotifySwipe(SwipeDirection.Left);
                }
                else
                {
                    float move = currentPosition.y - lastPointPosition.y;

                    if (move > 0)
                        NotifySwipe(SwipeDirection.Up);
                    else
                        NotifySwipe(SwipeDirection.Down);
                }

                isGottenSwipe = false;
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.W))
                NotifySwipe(SwipeDirection.Up);

            else if (Input.GetKeyDown(KeyCode.A))
                NotifySwipe(SwipeDirection.Left);

            else if (Input.GetKeyDown(KeyCode.D))
                NotifySwipe(SwipeDirection.Right);

            else if (Input.GetKeyDown(KeyCode.S))
                NotifySwipe(SwipeDirection.Down);
#endif
        }

        private Vector2 RectPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _swipeArea,
                Input.mousePosition,
                Camera.main,
                out var pos
            );
            return pos;
        }

        private void NotifySwipe(SwipeDirection direction)
        {
            OnSwipe?.Invoke(direction);
        }
    }
}