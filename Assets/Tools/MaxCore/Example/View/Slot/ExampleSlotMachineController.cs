using System;
using Tools.MaxCore.Tools.SerializableComponent;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Data;
using Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UIViews.Slot
{
    public class ExampleSlotMachineController : MonoBehaviour
    {
        [SerializeField] private SlotHandler _slotHandler;
        [SerializeField] private ExampleSlotMachineView _view;

        private int attempts;
        private bool IsEnoughAttempts => attempts > 0;

        public event Action OnRevertGame;

        private void Start()
        {
            attempts = 3;

            _slotHandler.CreateSlotMachine();

            _view.SetSpinButton(SpinSlot);

            _slotHandler.OnStartSpin += ActionsOnStart;
            _slotHandler.OnFinishSpin += CheckFinish;
            _slotHandler.OnGetWinSymbol += GetWinBonus;
        }

        private void OnDestroy()
        {
            _slotHandler.OnStartSpin -= ActionsOnStart;
            _slotHandler.OnFinishSpin -= CheckFinish;
            _slotHandler.OnGetWinSymbol -= GetWinBonus;
        }

        private void SpinSlot()
        {
            _slotHandler.NotifyStartSpin();
        }

        private void ActionsOnStart()
        {
            _view.DeactivateSpinButton();
            _view.ChangeCountText(--attempts);
        }

        private void CheckFinish()
        {
            if (!IsEnoughAttempts)
            {
                _view.SetLoseView();
            }

            _view.ActivateSpinButton();
        }

        private void GetWinBonus(SlotSymbolPayType slotSymbolPayType)
        {
            _view.SetWinView(() => { OnRevertGame?.Invoke(); });
            
            _slotHandler.OnFinishSpin -= CheckFinish;
        }
    }
}