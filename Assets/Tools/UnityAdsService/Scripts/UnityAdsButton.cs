using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.UnityAdsService.Scripts
{
    [RequireComponent(typeof(Button))]
    public class UnityAdsButton : MonoBehaviour
    {
        [SerializeField]
        private int _countAds;
        
        private UnityAdsService UnityAdsService => UnityAdsService.Instance;
        private Button WatchButton => GetComponent<Button>();

        private int _currentCountAds;
        
        public event Action<int> OnCanGetReward;

        private void Start()
        {
            Deactivate();

            _currentCountAds = _countAds;
            
            if (UnityAdsService.IsInitialize)
            {
                Initialize();
            }
            else
            {
                UnityAdsService.OnInitialize += Initialize;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeAllEvent();
            
            UnityAdsService.OnInitialize -= Initialize;
        }

        public void Deactivate()
        {
            WatchButton.interactable = false;
        }
        
        public void Activate()
        {
            WatchButton.interactable = true;
        }

        private void Initialize()
        {
            Activate();
            
            WatchButton.onClick.AddListener(ShowRewardedAd);
        }
        
        private void ShowRewardedAd()
        {
            var listener = UnityAdsService.ShowRewardedAd();
            
            if (listener != null)
            {
                _currentCountAds--;
                listener.OnShowCompleteAds += NotifyGetRewarded;
            }
        }

        private void NotifyGetRewarded()
        {
            if (_currentCountAds > 0)
            {
                Debug.Log(_currentCountAds);
                ShowRewardedAd();
            }
            else
            {
                _currentCountAds = _countAds;
                OnCanGetReward?.Invoke(_countAds);
            }
        }

        private void UnsubscribeAllEvent()
        {
            OnCanGetReward = null;
        }
    }
}