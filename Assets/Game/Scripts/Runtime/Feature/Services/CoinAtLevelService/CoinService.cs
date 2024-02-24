using System;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Services.CoinAtLevelService
{
    public class CoinService : MonoBehaviour
    {
        public int CurrentCoin { get; private set; }
        
        public event Action<int> OnChangeCoin;

        [Inject] private ResourceVault resourceVault;
        
        public void AddCoin(int value)
        {
            CurrentCoin += value;
            OnChangeCoin?.Invoke(CurrentCoin);
        }

        public void ResetCoin()
        {
            CurrentCoin = 0;
            OnChangeCoin?.Invoke(CurrentCoin);
        }
    }
}