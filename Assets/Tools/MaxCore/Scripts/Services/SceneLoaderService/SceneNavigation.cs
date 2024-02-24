using Tools.MaxCore.Scripts.ComponentHelp;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.SceneLoaderService
{
    public class SceneNavigation : MonoBehaviour
    {
        private const float FadeTime = 0.8f;
        
        [SerializeField] private ImageFader Fader;
        private SceneLoader sceneLoader = new SceneLoader();

        public void LoadLobby()
        {
            Fader.FadeTo(FadeTime, () => sceneLoader.LoadScene(SceneType.Lobby.ToString()));
        }

        public void LoadLevel()
        {
            Fader.FadeTo(FadeTime, () => sceneLoader.LoadScene(SceneType.Level.ToString()));
        }

        public void LoadSlotGame()
        {
            Fader.FadeTo(FadeTime, () => sceneLoader.LoadScene(SceneType.SlotMachineGame.ToString()));
        }
    }
}