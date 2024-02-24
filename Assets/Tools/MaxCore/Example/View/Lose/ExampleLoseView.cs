using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Services.Views.Lose
{
    public class ExampleLoseView : BaseView
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _againButton;

        [SerializeField] private Text _winTextCount;

        [Inject] private ExampleLoseController exampleLoseController;

        protected override void Initialize()
        {
            SetWinText();

            if (exampleLoseController.IsContinueGame)
            {
                _spinButton.interactable = false;
            }
        }

        protected override void Subscribe()
        {
            _spinButton.onClick.AddListener(OpenSlotGameView);
            _againButton.onClick.AddListener(ReloadGame);
            _menuButton.onClick.AddListener(BackToMenu);
        }

        private void OpenSlotGameView()
        {
            exampleLoseController.OpenSlotGameView();
            _spinButton.interactable = false;
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            _spinButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _againButton.onClick.RemoveAllListeners();
        }

        private void SetWinText()
        {
            _winTextCount.text = exampleLoseController.CountWin.ToString();
        }

        private void ReloadGame()
        {
            exampleLoseController.ReloadGame();
            DestroyView(.8f);
        }

        private void BackToMenu()
        {
            exampleLoseController.BackToMenu();
            DestroyView(.8f);
        }
    }
}