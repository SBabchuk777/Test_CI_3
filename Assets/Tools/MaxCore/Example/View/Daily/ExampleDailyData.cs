using System;
using Tools.MaxCore.Scripts.Services.DataHubService;

namespace Game.Scripts.Runtime.Services.Views.Daily
{
    [Serializable]
    public class ExampleDailyData : DataPayload
    {
        public DateTime LastRewardDate;
        public int ConsecutiveDays;

        public override void InitializeDefault()
        {
            LastRewardDate = default;
        }
    }
}