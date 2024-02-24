using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UiViews.Lose
{
    public class LoseView : BaseView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _againButton;

        [SerializeField] private Text _winTextCount;

        [Inject] private LoseController loseController;
        [Inject] private DataHub dataHub;

        protected override void Initialize()
        {
            SetWinText();

            if (!loseController.IsCanBuy || loseController.IsContinueGame)
            {
                _continueButton.interactable = false;     
            }
        }

        protected override void Subscribe()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _againButton.onClick.AddListener(ReloadGame);
            _menuButton.onClick.AddListener(BackToMenu);
        }

        private void ContinueGame()
        {
            loseController.ContinueGame();
            ClosePanel();
        }
        
        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            _menuButton.onClick.RemoveAllListeners();
            _againButton.onClick.RemoveAllListeners();
        }

        private void SetWinText()
        {
            _winTextCount.text = loseController.CountWin.ToString();
        }

        private void ReloadGame()
        {
            loseController.ReloadGame();
            DestroyView(.8f);
        }

        private void BackToMenu()
        {
            loseController.BackToMenu();
            DestroyView(.8f);
        }
    }
}