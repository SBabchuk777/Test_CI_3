using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.LevelSelect.ComponentUI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private ClosedLevelButton _closedState;
        [SerializeField] private AvailableLevelButton _availableState;
        [SerializeField] private ActiveLevelButton _activeState;

        [SerializeField] StarsPanel _starsPanel;

        private Button button;

        public void Initialize()
        {
            button = GetComponent<Button>();
            
            _closedState.Deactivate();
            _availableState.Deactivate();
            _activeState.Deactivate();
            
            _starsPanel.Deactivate();
        }
        
        public void SetSelectButton(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void SetClose(int value)
        {
            _closedState.Activate(SetCountLevelText(value));
            button.interactable = false;
        }

        public void SetAvailable(int value)
        {
            _availableState.Activate(SetCountLevelText(value));
            button.interactable = true;
        }

        public void SetActive(int countStar, int levelValue)
        {
            button.interactable = true;
            _activeState.Activate(SetCountLevelText(levelValue));
            _starsPanel.Activate();
            _starsPanel.SetActiveStars(countStar);
        }

        public void ResetButton()
        {
            button.onClick.RemoveAllListeners();
        }

        private string SetCountLevelText(int value)
        {
            return (value + 1).ToString();
        }
    }
}