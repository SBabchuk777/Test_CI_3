using DG.Tweening;
using Game.Scripts.Runtime.Feature.UiViews.Lose;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.SceneLoaderService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UiViews.Pause
{
    public class ExitPopUpView : BaseView
    {
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        [Inject] private LoseController loseController;
        [Inject] private UIViewService uiViewService;
        
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
            loseController.BackToMenu();
            DOVirtual.DelayedCall(.8f, ()=>  uiViewService.RemoveAllViews()).Play();
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
        }
    }
}