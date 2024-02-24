using System;
using System.Collections.Generic;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using Tools.MaxCore.Scripts.Services.ResourceVaultService;
using UnityEngine;

namespace Game.Scripts.Runtime.Services.Views.Daily
{
    public class DailyController : MonoBehaviour, IProjectInitializable
    {
        [SerializeField] private List<int> _rewardForDay;
        
        [Inject] private DataHub dataHub;
        [Inject] private ResourceVault resourceVault;
        
        private ExampleDailyData exampleDailyData;

        public bool IsGetRewardToday { get; private set; }
        public int CurrentReward => _rewardForDay[exampleDailyData.ConsecutiveDays];

        public event Action OnGetBonus;

        public void Initialize()
        {
            exampleDailyData = dataHub.LoadData<ExampleDailyData>(DataType.Daily);
            
            CheckLastReward();
            CheckCurrentReward();
        }


        private void CheckLastReward()
        {
            if (DateTime.Now.Date > exampleDailyData.LastRewardDate.Date.AddDays(1) ||
                exampleDailyData.ConsecutiveDays >= 6)
            {
                exampleDailyData.ConsecutiveDays = 0;
                SaveData();
            }
        }

        private void CheckCurrentReward()
        {
            IsGetRewardToday = DateTime.Now.Date <= exampleDailyData.LastRewardDate.Date;
        }

        public void GetReward()
        {
            exampleDailyData.ConsecutiveDays++;
            exampleDailyData.LastRewardDate = DateTime.Now.Date;

            resourceVault.AddResource(ResourceType.Coin, CurrentReward);
            IsGetRewardToday = true;

            SaveData();
            OnGetBonus?.Invoke();
        }

        private void SaveData()
        {
            dataHub.SaveData(DataType.Daily);
        }
    }
}