using System;
using Tools.MaxCore.Scripts.Services.DataHubService;

namespace Tools.MaxCore.Example.View.Settings
{
    [Serializable]
    public class SettingsData : DataPayload
    {
        public int SoundVolumeDefault;
        public int MusicVolumeDefault;

        public float SoundVolumeCount { get; set; }
        public float MusicVolumeCount { get; set; }

        public override void InitializeDefault()
        {
            MusicVolumeCount = MusicVolumeDefault;
            SoundVolumeCount = SoundVolumeDefault;
        }
    }
}