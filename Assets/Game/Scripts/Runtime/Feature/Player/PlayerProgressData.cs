using System;
using System.Collections.Generic;
using Tools.MaxCore.Scripts.Services.DataHubService;

namespace Game.Scripts.Runtime.Feature.Player
{
    [Serializable]
    public class PlayerProgressData : DataPayload
    {
        public int CurrentIDItem;
        public List<int> AvailableSkins;
        public bool IsShowTutorial;
    }
}