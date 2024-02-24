using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
	public class Tune
	{
		public AudioClip Clip { get; }
		public bool IsRepeatable { get; }
		public AudioType Type { get; }
		public float Speed { get; }

		public Tune(AudioClip clip, AudioType type, bool isRepeatable = false,  float speed = 1.0F)
		{
			Clip = clip;
			IsRepeatable = isRepeatable;
			Type = type;
			Speed = speed;
		}
	}
}
