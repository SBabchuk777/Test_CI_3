using System;
using Game.Scripts.Runtime.Feature.UIViews.Slot;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Services.Views.Lose
{
    public class ExampleLoseController : MonoBehaviour
    {
        [Inject] private UIViewService uiViewService;
        [Inject] private SceneNavigation sceneNavigation;
        [Inject] private DataHub dataHub;
        [Inject] private ResourceVault resourceVault;

        public bool IsContinueGame { get; set; }
        public int CountWin { get; set; }

        public event Action OnContinueGame;

        public void ReloadGame()
        {
            sceneNavigation.LoadLevel();
            resourceVault.AddResource(ResourceType.Coin, CountWin);
        }

        public void BackToMenu()
        {
            sceneNavigation.LoadLobby();
            resourceVault.AddResource(ResourceType.Coin, CountWin);
        }

        public void OpenSlotGameView()
        {
            var slotMachine = uiViewService.Instantiate(UIViewType.SlotMachine)
                .GetComponent<ExampleSlotMachineController>();

            slotMachine.OnRevertGame += NotifyComplete;
        }

        private void NotifyComplete()
        {
            uiViewService.RemoveAllViews();
            OnContinueGame?.Invoke();
        }
    }
}