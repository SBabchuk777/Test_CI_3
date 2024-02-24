using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.Services.CoinAtLevelService
{
    public class CoinPanel : MonoBehaviour
    {
        [SerializeField] private Text _countText;
        [SerializeField] private CoinService _coinService;

        private int currentCoin;

        private void Start()
        {
            _countText.text = 0.ToString();
            _coinService.OnChangeCoin += SetCoin;
        }

        private void SetCoin(int value)
        {
            DOTween.To(() => currentCoin, x => currentCoin = x, value, .4f)
                .OnUpdate(() => { _countText.text = currentCoin.ToString(); })
                .SetEase(Ease.OutQuad).Play();
        }
    }
}