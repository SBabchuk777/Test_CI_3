using UnityEngine;

namespace Models
{
    public class ControllerModel
    {
        private const string CoinsCountName = "CoinsCount";
        private const string TurnOnMusicName = "TurnOnMusic";
        private const string TurnOnSoundName = "TurnOnSound";
        
        public int CoinsCount
        {
            get => PlayerPrefs.HasKey(CoinsCountName) ? PlayerPrefs.GetInt(CoinsCountName) : 0;
            set => PlayerPrefs.SetInt(CoinsCountName, value);
        }

        public int TurnOnMusic
        {
            get => PlayerPrefs.HasKey(TurnOnMusicName) ? PlayerPrefs.GetInt(TurnOnMusicName) : 0;
            set => PlayerPrefs.SetInt(TurnOnMusicName, value);
        }
        
        public int TurnOnSound
        {
            get => PlayerPrefs.HasKey(TurnOnSoundName) ? PlayerPrefs.GetInt(TurnOnSoundName) : 0;
            set => PlayerPrefs.SetInt(TurnOnSoundName, value);
        }
    }
}
