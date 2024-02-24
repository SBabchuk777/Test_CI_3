using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Runtime.Feature.UIViews.Slot
{
    public class ExampleSlotMachineView : BaseView
    {
        [Multiline] public string _winValueText;
        [Multiline] public string _loseValueText;
        
        [SerializeField] private ExampleSpinMachineButton _exampleSpinButton;
        [SerializeField] private ExampleInformationPanel _exampleInformationPanel;

        protected override void Initialize()
        {
        }

        protected override void Subscribe()
        {
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            _exampleSpinButton.RemoveAllListeners();
        }

        public void SetSpinButton(UnityAction callback)
        {
            _exampleSpinButton.SetSpin(callback);
        }

        public void SetWinView(UnityAction callback)
        {
            _exampleInformationPanel.SetText(_winValueText);
            _exampleSpinButton.SetPlay(callback);
        }

        public void SetLoseView()
        {
            _exampleInformationPanel.SetText(_loseValueText);
            _exampleSpinButton.SetBack(()=> DestroyView());
        }

        public void ActivateSpinButton()
        {
            _exampleSpinButton.Interactable = true;
        }

        public void DeactivateSpinButton()
        {
            _exampleSpinButton.Interactable = false;
        }

        public void ChangeCountText(int value)
        {
            _exampleInformationPanel.SetCountText(value.ToString());
        }
    }
}