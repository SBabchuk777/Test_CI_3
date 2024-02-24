using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UiViews.Daily
{
    public class DailyView : BaseView
    {
        [SerializeField] private Button _getButton;
        [SerializeField] private Text _countRewardText;

        [Inject] private DailyController dailyController;
        protected override void Initialize()
        {
            ChangeText();
        }

        protected override void Subscribe()
        {
            _getButton.onClick.AddListener(GetReward);
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            _getButton.onClick.RemoveAllListeners();
        }

        private void GetReward()
        {
            dailyController.GetReward();
            ClosePanel();
        }

        private void ChangeText()
        {
            _countRewardText.text = dailyController.CurrentReward.ToString();
        }
    }
    
}