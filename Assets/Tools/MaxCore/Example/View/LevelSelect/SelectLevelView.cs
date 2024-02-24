using System.Collections.Generic;
using DG.Tweening;
using Tools.MaxCore.Example.View.LevelSelect.ComponentUI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UIViews.LevelSelect
{
    public class SelectLevelView : BaseView
    {
        public Button CloseButton;
        public List<LevelButton> LevelButtons;

        [Inject] private SelectLevelController selectLevelController;
        
        protected override void Initialize()
        {
            selectLevelController.PrepareView(LevelButtons);
        }

        protected override void Subscribe()
        {
            CloseButton.onClick.AddListener(ClosePanelInCurrentScene);
            selectLevelController.OnRunLevel += ClosePanelAfterLoadNextScene;
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            LevelButtons.ForEach(l => l.ResetButton());
            CloseButton.onClick.RemoveAllListeners();
            selectLevelController.OnRunLevel -= ClosePanelAfterLoadNextScene;
        }

        private void ClosePanelAfterLoadNextScene()
        {
            DOVirtual.DelayedCall(.8f, ClosePanel).Play();
        }

        private void ClosePanelInCurrentScene()
        {
            ClosePanel();
        }
    }
}