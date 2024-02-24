using Game.Scripts.Runtime.Feature.UiViews.Daily;
using Game.Scripts.Runtime.Feature.UiViews.Lose;
using Game.Scripts.Runtime.Feature.UiViews.Shop;
using Tools.MaxCore.Example.View.Settings;
using Tools.MaxCore.Scripts.ComponentHelp;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Services.Audio.AudioCore;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.Pause;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using Tools.MaxCore.Scripts.Services.SateMachine;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Project.DI
{
    public class ProjectInstaller : MonoBehaviour
    {
        public ImageFader SceneFader;
        public AudioService AudioService;
        public SettingService SettingService;
        public SceneNavigation SceneNavigation;
        public ProjectAudioPlayer ProjectAudioPlayer;
        public UIViewService UIViewService;
        public DataHub DataHub;
        public ResourceVault ResourceVault;

        [Header("Controllers in MVC")] 
        public ShopController ShopController;
        public DailyController DailyController;
        public LoseController LoseController;

        [Header("Game services")] 
        public GameStateMachine GameStateMachine;
        public PauseService PauseService;

        private DIContainer container;

        public DIContainer RegisterDependencies()
        {
            container = new DIContainer();
            container.Register(container);

            RegisterServices();
            RegisterControllers();

            return container;
        }

        private void RegisterControllers()
        {
            container.Register(ShopController);
            container.Register(DailyController);
            container.Register(LoseController);
            
            container.Register(GameStateMachine);
            container.Register(PauseService);
        }

        private void RegisterServices()
        {
            container.Register(SceneFader);
            container.Register(AudioService);
            container.Register(SettingService);
            container.Register(SceneNavigation);
            container.Register(ProjectAudioPlayer);
            container.Register(UIViewService);
            container.Register(DataHub);
            container.Register(ResourceVault);
        }
    }
}