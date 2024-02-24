using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
    public class AudioSourceFactory
    {
        private const string GameObjectName = "Sound";
        
        private GameObject audioParent;
        private GameObject sfxParent;
        private GameObject backgroundParent;
        private GameObject musicParent;
        private GameObject voiceMainParent;
        private GameObject voiceBackParent;


        private Dictionary<AudioType, Transform> audioTypeToParentMap;

        public void CreateComponent(Transform parent)
        {
            audioParent = new GameObject("AudioParent");
            audioParent.transform.SetParent(parent);
            
            Instantiate(ref sfxParent, "SfxParent");
            Instantiate(ref backgroundParent, "BackgroundParent");
            Instantiate(ref musicParent, "MusicParent");

            audioTypeToParentMap = new Dictionary<AudioType, Transform>()
            {
                { AudioType.Sfx, sfxParent.transform },
                { AudioType.Background, backgroundParent.transform },
                { AudioType.Music, musicParent.transform }
            };
        }

        public AudioSource Instantiate(Tune tune, AudioMixerGroup mixerGroup)
        {
            var createdObjectName = GameObjectName + " : " + tune.Clip.name;
            var audioSource = new GameObject(createdObjectName, typeof(AudioSource)).GetComponent<AudioSource>();

            audioSource.gameObject.transform.SetParent(audioTypeToParentMap[tune.Type]);
            audioSource.clip = tune.Clip;
            audioSource.loop = tune.IsRepeatable;
            audioSource.pitch = tune.Speed;
            audioSource.outputAudioMixerGroup = mixerGroup;

            return audioSource;
        }


        private void Instantiate(ref GameObject parent, string name)
        {
            parent = new GameObject(name);
            parent.transform.SetParent(audioParent.transform);
        }
    }
}