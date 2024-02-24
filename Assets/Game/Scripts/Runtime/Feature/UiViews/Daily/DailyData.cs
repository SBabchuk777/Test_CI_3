using System;
using Tools.MaxCore.Scripts.Services.DataHubService;

namespace Game.Scripts.Runtime.Feature.UiViews.Daily
{
    [Serializable]
    public class DailyData : DataPayload
    {
        public DateTime LastRewardDate;
        public int ConsecutiveDays;

        public override void InitializeDefault()
        {
            LastRewardDate = default;
        }
    }
}