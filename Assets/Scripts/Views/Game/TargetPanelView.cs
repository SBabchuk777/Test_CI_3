using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class TargetPanelView : MonoBehaviour
    {
        public Action<int> PressBtnAction { get; set; }

        [SerializeField] 
        private BoosterZoneView _boosterZoneView;
        [SerializeField] 
        private Text _targetText;
        [SerializeField] 
        private Text _countLeveltext;
        [SerializeField] 
        private Button _startBtn;
        [SerializeField] 
        private Button _backBtn;

        private void OnEnable()
        {
            _startBtn.onClick.AddListener(delegate { NotificationPressBtn(0); });
            _backBtn.onClick.AddListener(delegate { NotificationPressBtn(1); });
        }

        private void OnDisable()
        {
            _startBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
        }

        public void UpdateTexts(int countLevel, int target, int countFirstBooster, int countSecondBooster)
        {
            _countLeveltext.text = "Level " + countLevel;
            _targetText.text = target + " EX";
            _boosterZoneView.UpdateFirstBoosterCountText(countFirstBooster);
            _boosterZoneView.UpdateSecondBoosterCountText(countSecondBooster);
        }

        private void NotificationPressBtn(int value)
        {
            PressBtnAction.Invoke(value);
        }
    }
}
