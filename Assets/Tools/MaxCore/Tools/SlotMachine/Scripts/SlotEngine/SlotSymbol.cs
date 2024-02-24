using System;
using DG.Tweening;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Data;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    public class SlotSymbol : MonoBehaviour
    {
        public SlotSymbolLevelType SymbolID;
        public SlotSymbolPayType PaySymbolID;

        public SpriteRenderer spriteLight;
        public SpriteRenderer spriteSymbol;

        public void TurnOnLight()
        {
            if (spriteLight != null)
                spriteLight.gameObject.SetActive(true);
        }

        public void TurnOffLight()
        {
            if (spriteLight != null)
                spriteLight.gameObject.SetActive(false);
        }

        public void PlayWinAnimation(float timeAnimation, Action callback = null)
        {
            var localScale = spriteSymbol.transform.localScale;

            DOTween.Sequence()
                .Append(spriteSymbol.transform.DOScale(localScale + new Vector3(0.1f, 0.1f, 0), timeAnimation / 2))
                .Append(spriteSymbol.transform.DOScale(localScale - new Vector3(0.1f, 0.1f, 0), timeAnimation / 2))
                .OnComplete(() =>
                {
                    spriteSymbol.transform.localScale = localScale;
                    callback?.Invoke();
                })
                .Play();
        }
    }
}