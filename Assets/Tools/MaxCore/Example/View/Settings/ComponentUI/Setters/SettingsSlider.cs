using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Settings.ComponentUI.Setters
{
    public class SettingsSlider : SoundSetter
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;


        public override void Initialize()
        {
            _musicSlider.value = GetSoundValue(MusicValue, MaxMusicValueMixer);
            _soundSlider.value = GetSoundValue(SoundValue, MaxSoundValueMixer);
            
            _musicSlider.onValueChanged.AddListener(NotifyMusic);
            _soundSlider.onValueChanged.AddListener(NotifySound);
        }

        private void NotifySound(float value)
        {
            var interValue = Mathf.Lerp(MinValueMixer, MaxSoundValueMixer, value);
            
            NotifyChangeSound(interValue);
        }
        private void NotifyMusic(float value)
        {
            var interValue = Mathf.Lerp(MinValueMixer, MaxMusicValueMixer, value);

            NotifyChangeMusic(interValue);
        }

        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveAllListeners();
            _soundSlider.onValueChanged.RemoveAllListeners();
        }
        

        private float GetSoundValue(float soundValue, int maxValue)
        {
            return (soundValue - (-MinValueMixer)) / (maxValue - (-MinValueMixer));
        }
    }
}