using System;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Audio;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Data;
using Tools.MaxCore.Tools.SlotMachine.Scripts.WinStatus;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    public class SlotHandler : MonoBehaviour
    {
        public SlotController SlotController;
        public WinCalculator WinStatus;
        public SlotAudioPlayer AudioPlayer;
        
        public bool IsSpin { get; private set; }
        
        public event Action OnStartSpin;
        public event Action OnStopSpin;
        public event Action OnFinishSpin;
        public event Action<SlotSymbolPayType> OnGetWinSymbol;
        public event Action OnLose;

        public void CreateSlotMachine()
        {
            WinStatus.Init();
            SlotController.Create();
        }

        public void NotifyWinBonus(SlotSymbolPayType slotSymbolPayType)
        {
            OnGetWinSymbol?.Invoke(slotSymbolPayType);
        }
        public void NotifyLose()
        {
            OnLose?.Invoke();
        }
        public void NotifyStartSpin()
        {
            AudioPlayer.PlayAudioMoveSlot();
            OnStartSpin?.Invoke();
            IsSpin = true;
        }

        public void NotifyStopSpin()
        {
            AudioPlayer.StopAudioMoveSlot();
            OnStopSpin?.Invoke();
        }

        public void NotifyFinishSpin()
        {
            IsSpin = false;
            OnFinishSpin?.Invoke();
        }
    }
}