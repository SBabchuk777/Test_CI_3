using DG.Tweening;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Services.Views.Leave
{
    public class ExampleLeaveGame : BaseView
    {
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        [Inject] private SceneNavigation sceneNavigation;
        protected override void Initialize()
        {
        }

        protected override void Subscribe()
        {
            _noButton.onClick.AddListener(ClosePanel);
            _yesButton.onClick.AddListener(BackToMenu);
        }

        private void BackToMenu()
        {
            sceneNavigation.LoadLobby();
            DOVirtual.DelayedCall(.8f, ()=> DestroyView()).Play();
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
        }
    }
}