using System;
using System.Collections.Generic;
using Game.Scripts.Runtime.Feature.UIViews.LevelSelect.Data;
using Tools.MaxCore.Scripts.Services.DataHubService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.UIViews.LevelSelect
{
    [Serializable]
    public class SelectLevelData : DataPayload
    {
        [SerializeField] private int countLevels;
        public List<LevelInformation> Levels;

        public override void InitializeDefault()
        {
            //Levels = new List<LevelInformation>();
            
            /*for (var i = 0; i < countLevels; i++)
            {
                Levels.Add(new LevelInformation { CountLevel = i, _levelSelectState = LevelSelectState.Closed });
            }
            
            Levels[0]._levelSelectState = LevelSelectState.Available;*/
        }
    }
}