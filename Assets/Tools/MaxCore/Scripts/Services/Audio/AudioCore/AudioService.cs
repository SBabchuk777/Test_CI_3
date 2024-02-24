using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
	public class AudioService : MonoBehaviour, IProjectInitializable
	{
		[SerializeField] private AudioServiceData Data;
		
		private AudioSourceFactory audioSourceFactory;

		public void Initialize()
		{
			audioSourceFactory = new AudioSourceFactory();
			audioSourceFactory.CreateComponent(transform);
		}
		
		public AudioSource Play(Tune tune)
		{
			var audioSource = audioSourceFactory.Instantiate(tune, Data.AudioTypeToMixerGroupMap[tune.Type]);
			audioSource.Play();

			if (!tune.IsRepeatable)
			{
				var timeToDestroy = Mathf.Abs(tune.Clip.length / tune.Speed);

				Destroy(audioSource.gameObject, timeToDestroy);
			}

			return audioSource;
		}

		public void Stop(AudioSource source)
		{
			source.Stop();
			Object.Destroy(source.gameObject);
		}

		public void SetVolume(AudioType type, float value) =>
			Data.Mixer.SetFloat(type.ToString(), value);
		
		public float GetVolume(AudioType type)
		{
			if (Data.Mixer.GetFloat(type.ToString(), out var value))
				return value;
			
			throw new AudioServiceException("Dont have this group in mixer");
		}
	}
}
