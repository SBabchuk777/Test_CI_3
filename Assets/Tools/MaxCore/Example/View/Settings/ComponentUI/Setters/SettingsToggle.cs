using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Settings.ComponentUI.Setters
{
    public class SettingsToggle : SoundSetter
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        
        [Inject] private ProjectAudioPlayer audioPlayer;
        
        public override void Initialize()
        {
            if (_soundToggle != null)
            {
                _soundToggle.isOn = SoundValue == MaxSoundValueMixer;
                _soundToggle.onValueChanged.AddListener(NotifySound);
            }

            if (_musicToggle != null)
            {
                _musicToggle.isOn = MusicValue == MaxMusicValueMixer;
                _musicToggle.onValueChanged.AddListener(NotifyMusic);
            }
        }

        private void OnDestroy()
        {
            _soundToggle?.onValueChanged.RemoveAllListeners();
            _musicToggle?.onValueChanged.RemoveAllListeners();
        }

        private void NotifySound(bool isOn)
        {
            if (isOn)
            {
                audioPlayer.PlayAudioSfx(ProjectAudioType.Click);
            }
            
            NotifyChangeSound(isOn ? MaxSoundValueMixer : MinValueMixer);
        }
        
        private void NotifyMusic(bool isOn)
        {
            if (isOn)
            {
                audioPlayer.PlayAudioSfx(ProjectAudioType.Click);
            }
            
            NotifyChangeMusic(isOn ? MaxMusicValueMixer : MinValueMixer);
        }
    }
}