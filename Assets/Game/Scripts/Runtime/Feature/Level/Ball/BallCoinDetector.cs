using Game.Scripts.Runtime.Feature.Level.Component;
using Game.Scripts.Runtime.Feature.Services.CoinAtLevelService;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Ball
{
    public class BallCoinDetector : MonoBehaviour
    {
        [SerializeField] private CoinService _coinService;

        [Inject] private ProjectAudioPlayer audioPlayer;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Coin>())
            {
                Destroy(col.gameObject);
                //audioPlayer.PlayAudioSfx(add coin);
                _coinService.AddCoin(1);
            }
        }
    }
}