using System;
using Game.Scripts.Runtime.Feature.UiViews.Daily;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.Lobby
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private LobbyArbiter _lobbyArbiter;
        [SerializeField] private Button _dailyButton;
        [SerializeField] private Button _playButton;
        
        [Inject] private DailyController dailyController;
      
        private void Start()
        {
            SetDailyButton();
            
            _playButton.onClick.AddListener(_lobbyArbiter.RunGame);
        }

        private void OnDestroy()
        {
            dailyController.OnGetBonus -= SetDailyButton;
        }

        private void SetDailyButton()
        {
            if (dailyController.IsGetRewardToday)
            {
                _dailyButton.gameObject.SetActive(false);
            }
            else
            {
                _dailyButton.gameObject.SetActive(true);
                dailyController.OnGetBonus += SetDailyButton;
            }
        }
        
    }
}