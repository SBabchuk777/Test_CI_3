using System;
using System.Collections.Generic;
using DG.Tweening;
using Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.WinStatus
{
    public class WinAnimator : MonoBehaviour
    {
        public float TimeShowLine;
        public float AwaitTimeAfterShowLine;
        
        public void PlayWinAnimation(List<Tuple<List<SlotSymbol>, LineGroup>> lineWin, Action callbackFinish = null, Action callbackLine = null)
        {
            var animationSequence = DOTween.Sequence();

            foreach (var line in lineWin)
            {
                var lineAnimation = DOTween.Sequence();
                
                lineAnimation.Append(DOVirtual.DelayedCall(AwaitTimeAfterShowLine, () => {callbackLine?.Invoke(); }));
                
                foreach (var caster in line.Item2.RayCasters)
                    lineAnimation.Append(DOVirtual.DelayedCall(0, () => caster.GetSymbol().TurnOnLight()));

                foreach (var symbol in line.Item1)
                    lineAnimation.Append(DOVirtual.DelayedCall(0, () => symbol.PlayWinAnimation(TimeShowLine)));

                lineAnimation.Append(DOVirtual.DelayedCall(TimeShowLine, () => { }));

                foreach (var caster in line.Item2.RayCasters)
                    lineAnimation.Append(DOVirtual.DelayedCall(0, () => caster.GetSymbol().TurnOffLight()));
                
                animationSequence.Append(lineAnimation);
            }

            animationSequence.OnComplete(() => callbackFinish?.Invoke())
                .Play();
        }
    }
}