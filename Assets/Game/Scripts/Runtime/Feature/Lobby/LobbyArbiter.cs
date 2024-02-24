using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.ComponentHelp;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.SateMachine;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Lobby
{
    public class LobbyArbiter : MonoBehaviour
    {
        [Inject] private UIViewService uiViewService;
        [Inject] private ImageFader sceneFader;
        [Inject] private SceneNavigation sceneNavigation;
        [Inject] private DataHub dataHub;
        [Inject] private ProjectAudioPlayer audioPlayer;
        [Inject] private GameStateMachine gameStateMachine;
        
        private void Start()
        {
            Initialize();
            sceneFader.FadeOutAwait(1f, .5f);
        }

        private void Initialize()
        {
            gameStateMachine.SetLevelState(LevelState.Lobby);
            audioPlayer.PlayAudioAmbientOnLobby();
            audioPlayer.StopAudioAmbientOnLevel();
        }
        
        public void RunGame()
        {
            sceneNavigation.LoadLevel();
        }
        
    }
}