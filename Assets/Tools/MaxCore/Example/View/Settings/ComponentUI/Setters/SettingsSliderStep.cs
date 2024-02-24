using UnityEngine;

namespace Tools.MaxCore.Example.View.Settings.ComponentUI
{
    public class SettingsSliderStep : SoundSetter
    {
        [SerializeField] private SliderStep _sliderMusicStep;
        [SerializeField] private SliderStep _sliderSoundStep;
        
        public override void Initialize()
        {
            _sliderMusicStep.Initialize(MusicValue, MinValueMixer, MaxMusicValueMixer);
            _sliderSoundStep.Initialize(SoundValue, MinValueMixer, MaxSoundValueMixer);
            
            _sliderMusicStep.OnChangValue += NotifyChangeMusic;
            _sliderSoundStep.OnChangValue += NotifyChangeSound;
        }
    }
}