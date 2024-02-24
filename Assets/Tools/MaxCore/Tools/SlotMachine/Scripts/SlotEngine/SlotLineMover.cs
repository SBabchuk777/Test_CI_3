using System;
using DG.Tweening;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    public class SlotLineMover : MonoBehaviour
    {
        private float currentValue;
        
        public void Move(float MaxSpeed, float increaseRate)
        {
            if (currentValue <= MaxSpeed)
            {
                currentValue += increaseRate * Time.deltaTime;
                currentValue = Mathf.Min(currentValue, MaxSpeed);
            }
            
            transform.position -= new Vector3(0, 1, 0) * (currentValue * Time.deltaTime);
        }

        public void MoveTo(float position, Action callback) =>
            transform.DOMove(transform.position + new Vector3(0, position, 0), 0.3f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    currentValue = 0;
                    callback?.Invoke();
                })
                .Play();
    }
}