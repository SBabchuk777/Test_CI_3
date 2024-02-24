using System;
using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine.Audio;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
	[Serializable]
	public class AudioTypeToMixerGroupMap : SerializableDictionary<AudioType, AudioMixerGroup>
	{

	}
}
