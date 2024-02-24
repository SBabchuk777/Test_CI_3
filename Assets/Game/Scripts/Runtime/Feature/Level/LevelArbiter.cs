using DG.Tweening;
using Game.Scripts.Runtime.Feature.Player;
using Game.Scripts.Runtime.Feature.Services.CoinAtLevelService;
using Game.Scripts.Runtime.Feature.UiViews.Lose;
using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.ComponentHelp;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.Pause;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using Tools.MaxCore.Scripts.Services.SateMachine;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class LevelArbiter : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private LevelHandler _levelHandler;

        [SerializeField] private CoinService _coinService;

        [Inject] private ImageFader sceneFader;
        [Inject] private SceneNavigation sceneNavigation;
        [Inject] private UIViewService uiViewService;
        [Inject] private ProjectAudioPlayer audioPlayer;
        [Inject] private GameStateMachine gameStateMachine;
        [Inject] private LoseController loseController;
        [Inject] private DataHub dataHub;
        [Inject] private PauseService pauseService;
        [Inject] private ResourceVault resourceVault;

        public void Start()
        {
            Subscribe();
            Initialize();

            sceneFader.FadeOutAwait(1, .5f);
        }

        private void Subscribe()
        {
            _levelHandler.OnLose += OpenLoseView;
            loseController.OnContinueGame += AwaitRunGame;
        }

        private void OnDestroy()
        {
            _levelHandler.OnLose -= OpenLoseView;
            loseController.OnContinueGame -= AwaitRunGame;
        }

        private void Initialize()
        {
            OpenTutorial();
            _levelBuilder.Initialize();
            audioPlayer.PlayAudioAmbientOnLevel();
            audioPlayer.StopAudioAmbientOnLobby();
        }

        private void ContinueLevel()
        {
            _coinService.ResetCoin();
            _levelBuilder.ContinueLevel();
            pauseService.FinishPause();
            gameStateMachine.SetLevelState(LevelState.Game);
        }

        private void RunLevel()
        {
            gameStateMachine.SetLevelState(LevelState.Game);
        }

        private void AwaitRunGame()
        {
            loseController.IsContinueGame = true;

            DOVirtual.DelayedCall(0.5f, ContinueLevel).Play();
        }

        private void OpenLoseView()
        {
            var instance = uiViewService.Instantiate(UIViewType.Lose);
            instance.OnCloseView += () => { pauseService.FinishPause(); };
            
            pauseService.StartPause();
            audioPlayer.PlayAudioSfx(ProjectAudioType.Lose);
            gameStateMachine.SetLevelState(LevelState.Lose);
            
            loseController.CountWin = _coinService.CurrentCoin;
            resourceVault.AddResource(ResourceType.Coin, _coinService.CurrentCoin);
        }

        public void OpenPauseView()
        {
            var instance = uiViewService.Instantiate(UIViewType.Pause);
            instance.OnCloseView += () => { pauseService.FinishPause(); };
            pauseService.StartPause();
        }

        private void OpenTutorial()
        {
            if (!dataHub.LoadData<PlayerProgressData>(DataType.Progress).IsShowTutorial)
            {
                var instance = uiViewService.Instantiate(UIViewType.Tutorial);
                instance.OnCloseView += RunLevel;
            }
            else
            {
                DOVirtual.DelayedCall(2.5f, RunLevel).Play();
            }
        }
    }
}