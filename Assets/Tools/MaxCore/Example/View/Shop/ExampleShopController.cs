using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Runtime.Feature.Player;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;

namespace Tools.MaxCore.Example.View.Shop
{
    public class ExampleShopController : MonoBehaviour, IProjectInitializable
    {
        [SerializeField] private ShopItemInfoData _skinInfoData;

        [Inject] private DataHub dataHub;
        [Inject] private ProjectAudioPlayer audioPlayer;
        [Inject] private UIViewService uiViewService;
        [Inject] private ResourceVault resourceVault;

        private PlayerProgressData progressData;
        public Dictionary<int, ItemInfo> CurrentItemInfo { get; private set; }
        
        public event Action OnChangePanel;
    
        public void Initialize()
        {
            progressData = dataHub.LoadData<PlayerProgressData>(DataType.Progress);
        }

        public void PrepareItemInfoMap()
        {
            CurrentItemInfo = new Dictionary<int, ItemInfo>();

            foreach (var skinInfo in _skinInfoData.PathMap)
            {
                CurrentItemInfo.Add(skinInfo.Key, new ItemInfo()
                {
                    Skin = skinInfo.Value.Skin,
                    Count = skinInfo.Value.Count,
                    ID = skinInfo.Key
                });
            }

            foreach (var skinID in progressData.AvailableSkins)
                CurrentItemInfo[skinID].IsPurchased = true;
            
            foreach (var skinInfo in CurrentItemInfo
                         .Where(i => !i.Value.IsPurchased && resourceVault.IsEnoughResource(ResourceType.Coin, i.Value.Count)))
            {
                skinInfo.Value.IsCanPurchased = true;
            }

            CurrentItemInfo[progressData.CurrentIDItem].IsSelected = true;

        }

        public void PurchaseItem(int itemID)
        {
            progressData.AvailableSkins.Add(itemID);
            resourceVault.SpendResource(ResourceType.Coin, CurrentItemInfo[itemID].Count);
            audioPlayer.PlayAudioSfx(ProjectAudioType.Purchase);
            SetCurrentItem(itemID);
        }

        public void SetCurrentItem(int itemID)
        {
            progressData.CurrentIDItem = itemID;
            PrepareItemInfoMap();
            dataHub.SaveData(DataType.Progress);
            
            OnChangePanel?.Invoke();
        }
    }
}