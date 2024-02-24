using UnityEngine;

namespace Models.Game
{
    public class GameModel
    {
        private const string LevelPrefsName = "CurrentLevel";
        private const string SameCellsBoosterCountName = "SameCellsBoosterCount";
        private const string OneColumnBoosterCountName = "OneColumnBoosterCount";
        private const string TotalSecondsName = "TotalSeconds";

        public int CurrentLevel
        {
            get => PlayerPrefs.HasKey(LevelPrefsName) ? PlayerPrefs.GetInt(LevelPrefsName) : 1;
            set => PlayerPrefs.SetInt(LevelPrefsName, value);
        }

        public int SameCellsBoosterCount
        {
            get => PlayerPrefs.HasKey(SameCellsBoosterCountName) ? PlayerPrefs.GetInt(SameCellsBoosterCountName) : 1;
            set => PlayerPrefs.SetInt(SameCellsBoosterCountName, value);
        }
        
        public int OneColumnBoosterCount
        {
            get => PlayerPrefs.HasKey(OneColumnBoosterCountName) ? PlayerPrefs.GetInt(OneColumnBoosterCountName) : 1;
            set => PlayerPrefs.SetInt(OneColumnBoosterCountName, value);
        }

        public int TotalSeconds
        {
            get => PlayerPrefs.GetInt(TotalSecondsName);
            set => PlayerPrefs.SetInt(TotalSecondsName, value);
        }

        public int RemainderTarget(int currentScore)
        {
            int remainderTarget = currentScore - CurrentLevel * 100;

            return remainderTarget;
        }
    }
}
