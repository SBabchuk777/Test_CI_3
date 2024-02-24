using System;

namespace Game.Scripts.Runtime.Feature.UIViews.LevelSelect.Data
{
    [Serializable]
    public class LevelInformation
    {
        public int CountLevel;
        
        public LevelSelectState LevelSelectState;
        public int CountStarInLevel;
    }
}