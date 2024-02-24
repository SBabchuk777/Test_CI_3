using System;
using Game.Scripts.Runtime.Feature.UIViews.Slot;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using Tools.MaxCore.Scripts.Services.SateMachine;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UiViews.Lose
{
    public class LoseController : MonoBehaviour
    {
        [Inject] private UIViewService uiViewService;
        [Inject] private SceneNavigation sceneNavigation;
        [Inject] private DataHub dataHub;
        [Inject] private ResourceVault resourceVault;
        [Inject] private GameStateMachine gameStateMachine;

        public bool IsContinueGame { get; set; }
        public int CountWin { get; set; }
        public bool IsCanBuy => resourceVault.GetResourceAmount(ResourceType.Coin) >= 50;

        public event Action OnContinueGame;

        public void ReloadGame()
        {
            sceneNavigation.LoadLevel();

            IsContinueGame = false;
        }

        public void BackToMenu()
        {
            sceneNavigation.LoadLobby();

            IsContinueGame = false;
        }

        public void ContinueGame()
        {
            resourceVault.SpendResource(ResourceType.Coin, 50);
            OnContinueGame?.Invoke();
        }

        private void NotifyContinue()
        {
            uiViewService.RemoveAllViews();
            OnContinueGame?.Invoke();
        }
    }
}