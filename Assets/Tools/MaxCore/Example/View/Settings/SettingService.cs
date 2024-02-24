using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.Audio.AudioCore;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using UnityEngine;
using AudioType = Tools.MaxCore.Scripts.Services.Audio.AudioCore.AudioType;

namespace Tools.MaxCore.Example.View.Settings
{
    public class SettingService : MonoBehaviour, IProjectInitializable
    {
        public SettingsData SettingsData { get; private set; }

        [Inject] private DataHub dataHub;
        [Inject] private AudioService audioService;

        public void Initialize()
        {
            SettingsData = dataHub.LoadData<SettingsData>(DataType.Settings);
        }

        public void ChangeSound(float value)
        {
            audioService.SetVolume(AudioType.Music, value);
            audioService.SetVolume(AudioType.Sfx, value);
            
            SettingsData.SoundVolumeCount = value;
        }

        public void ChangeMusic(float value)
        {
            audioService.SetVolume(AudioType.Background, value);
            
            SettingsData.MusicVolumeCount = value;
        }
        
        public void SaveData() =>
            dataHub.SaveData(DataType.Settings, SettingsData);
    }
}