using UnityEngine;
using UnityEngine.Audio;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
	[CreateAssetMenu(fileName = "AudioServiceData", menuName = "Game/Services/AudioServiceData", order = 1)]
	public class AudioServiceData : ScriptableObject
	{
		public AudioMixer Mixer;
		public AudioTypeToMixerGroupMap AudioTypeToMixerGroupMap;
	}
}
