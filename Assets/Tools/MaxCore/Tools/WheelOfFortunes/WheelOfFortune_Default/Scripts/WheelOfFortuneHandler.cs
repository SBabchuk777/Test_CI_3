using System;
using DG.Tweening;
using UnityEngine;

namespace Tools.MaxCore.Tools.WheelOfFortunes.WheelOfFortune_Default.Scripts
{
    public class WheelOfFortuneHandler : MonoBehaviour
    {
        [SerializeField] private Wheel _wheel;
        [SerializeField] private WheelRayCaster _wheelRayCaster;

        public event Action OnStartSpin;
        public event Action<CellType> OnFinishSpin;

        private void OnDestroy()
        {
            UnsubscribeAllHandlers();
        }

        public void Initialize()
        {
            _wheel.OnStop += NotifyStopSpin;
        }
        public void Spin()
        {
            DOVirtual.DelayedCall(0.3f, () => _wheel.SpinWheel())
                .Play();
            
            OnStartSpin?.Invoke();
        }

        public void UnsubscribeAllHandlers()
        {
            OnStartSpin = null;
            OnFinishSpin = null;
        }
        
        private void NotifyStopSpin()
        {
            OnFinishSpin?.Invoke(_wheelRayCaster.GetCellType());
        }
    }
}