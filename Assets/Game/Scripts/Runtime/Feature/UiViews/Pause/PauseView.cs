using Game.Scripts.Runtime.Feature.UiViews.Lose;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UiViews.Pause
{
    public class PauseView : BaseView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        [Inject] private UIViewService uiViewService;
        [Inject] private LoseController loseController;
        protected override void Initialize()
        {
        }

        protected override void Subscribe()
        {
            _playButton.onClick.AddListener(ClosePanel);
            _restartButton.onClick.AddListener(()=>
            {
                DestroyView(0.8f);
                loseController.ReloadGame();
            });
            _exitButton.onClick.AddListener(() => uiViewService.Instantiate(UIViewType.ExitPopUp, 1));
        }

        protected override void Open()
        {
            
        }

        protected override void Unsubscribe()
        {
            _playButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}