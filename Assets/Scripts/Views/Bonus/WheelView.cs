using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Views.Bonus
{
    public class WheelView : MonoBehaviour
    {
        public Action<int> RotationEndAction
        {
            get;
            set;
        }

        [SerializeField] 
        private List<int> _angles;
        [SerializeField] 
        private List<int> _prizes;
        [SerializeField] 
        private RectTransform _wheelRect;
        [SerializeField] 
        private int _speed;

        private int _randomIndex;

        public void StartRotateWheel()
        {
            _randomIndex = 0;

            _randomIndex = Random.Range(0, _angles.Count);

            _wheelRect.DORotate(new Vector3(0, 0, 720 + _angles[_randomIndex]), _speed, RotateMode.WorldAxisAdd)
                .OnComplete(RotationEnd);
        }

        private void RotationEnd()
        {
            RotationEndAction.Invoke(_prizes[_randomIndex]);
        }
    }
}