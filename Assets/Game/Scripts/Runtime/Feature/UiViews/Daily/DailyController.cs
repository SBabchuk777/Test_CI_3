using System;
using System.Collections.Generic;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UiViews.Daily
{
    public class DailyController : MonoBehaviour, IProjectInitializable
    {
        [SerializeField] private List<int> _rewardForDay;
        
        [Inject] private DataHub dataHub;
        [Inject] private ResourceVault resourceVault;
        
        private DailyData dailyData;

        public bool IsGetRewardToday { get; private set; }
        public int CurrentReward => _rewardForDay[dailyData.ConsecutiveDays];

        public event Action OnGetBonus;

        public void Initialize()
        {
            dailyData = dataHub.LoadData<DailyData>(DataType.Daily);
            
            CheckLastReward();
            CheckCurrentReward();
        }


        private void CheckLastReward()
        {
            if (DateTime.Now.Date > dailyData.LastRewardDate.Date.AddDays(1) ||
                dailyData.ConsecutiveDays >= 6)
            {
                dailyData.ConsecutiveDays = 0;
                SaveData();
            }
        }

        private void CheckCurrentReward()
        {
            IsGetRewardToday = DateTime.Now.Date <= dailyData.LastRewardDate.Date;
        }

        public void GetReward()
        {
            resourceVault.AddResource(ResourceType.Coin, CurrentReward);
            IsGetRewardToday = true;
            
            dailyData.ConsecutiveDays++;
            dailyData.LastRewardDate = DateTime.Now.Date;


            SaveData();
            OnGetBonus?.Invoke();
            OnGetBonus = null;
        }

        private void SaveData()
        {
            dataHub.SaveData(DataType.Daily, dailyData);
        }
    }
}