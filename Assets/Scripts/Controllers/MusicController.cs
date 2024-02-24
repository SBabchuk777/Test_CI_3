using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] 
    private AudioSource _musicSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMusic()
    {
        if (_musicSource.isPlaying)
        {
            return;
        }
        
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
