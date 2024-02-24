using DG.Tweening;
using Tools.MaxCore.Example.View.Settings;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.Audio.AudioCore;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using UnityEngine;
using AudioType = Tools.MaxCore.Scripts.Services.Audio.AudioCore.AudioType;

namespace Tools.MaxCore.Scripts.Project.Audio
{
    public class ProjectAudioPlayer : MonoBehaviour, IProjectInitializable
    {
        public ProjectAudioPath ProjectAudioPath;

        [Inject] private AudioService audioService;
        [Inject] private DataHub dataHub;

        private AudioSource lobbyAmbientSource;
        private AudioSource levelAmbientSource;

        private SettingsData SettingsData { get; set; }
        
        public void Initialize()
        {
            SettingsData = dataHub.LoadData<SettingsData>(DataType.Settings);
            
            audioService.SetVolume(AudioType.Background, SettingsData.MusicVolumeCount);
            audioService.SetVolume(AudioType.Music, SettingsData.SoundVolumeCount);
            audioService.SetVolume(AudioType.Sfx, SettingsData.SoundVolumeCount);
        }
        
        public void PlayAudioAmbientOnLobby()
        {
            if (lobbyAmbientSource != null)
            {
                return;
            }
            var audioClip = ProjectAudioPath.ProjectAudioPathMap[ProjectAudioType.LobbyAmbient];

            lobbyAmbientSource = audioService.Play(new Tune(audioClip, AudioType.Background, true));
            lobbyAmbientSource.volume = 0;
            lobbyAmbientSource.DOFade(1, 3f).Play();
        }

        public void StopAudioAmbientOnLobby()
        {
            if (lobbyAmbientSource != null)
                audioService.Stop(lobbyAmbientSource);
        }
        public void PlayAudioAmbientOnLevel()
        {
            if (levelAmbientSource != null)
            {
                return;
            }
            var audioClip = ProjectAudioPath.ProjectAudioPathMap[ProjectAudioType.LevelAmbient];

            levelAmbientSource = audioService.Play(new Tune(audioClip, AudioType.Background, true));
            levelAmbientSource.volume = 0;
            levelAmbientSource.DOFade(1, 3f).Play();
        }

        public void StopAudioAmbientOnLevel()
        {
            if (levelAmbientSource != null)
                audioService.Stop(levelAmbientSource);
        }
        
        public void PlayAudioSfx(ProjectAudioType projectAudioType)
        {
            audioService.Play(new Tune(ProjectAudioPath.ProjectAudioPathMap[projectAudioType], AudioType.Sfx));
        } 
        
        public void PlayAudioMusic(ProjectAudioType projectAudioType)
        {
            audioService.Play(new Tune(ProjectAudioPath.ProjectAudioPathMap[projectAudioType], AudioType.Music));
        }
    }
}