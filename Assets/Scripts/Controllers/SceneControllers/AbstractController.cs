using Models;
using UnityEngine;

namespace Controllers.SceneControllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _soundSource;
        
        private ControllerModel _model;
        private MusicController _musicController;

        protected int CoinCount
        {
            get => _model.CoinsCount;
            set => _model.CoinsCount = value;
        }

        protected int TurnOnMusic
        {
            get => _model.TurnOnMusic;
            set => _model.TurnOnMusic = value;
        }

        protected int TurnOnSound
        {
            get => _model.TurnOnSound;
            set => _model.TurnOnSound = value;
        }

        private void OnEnable()
        {
            _model = new ControllerModel();
            _musicController = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>();
            
            OnEnableScene();
        }

        private void Start()
        {
            OnStartScene();
            
            PlayMusic();
        }

        private void OnDisable()
        {
            OnDisableScene();
        }

        protected abstract void OnEnableScene();

        protected abstract void OnStartScene();

        protected abstract void OnDisableScene();

        protected void PlaySound(AudioClip clip)
        {
            if (TurnOnSound == 0)
            {
                _soundSource.clip = clip;
                _soundSource.Play();
            }
        }

        protected void ChangeTurnSound()
        {
            TurnOnSound = TurnOnSound == 0 ? 1 : 0;
        }

        protected void ChangeTurnMusic()
        {
            TurnOnMusic = TurnOnMusic == 0 ? 1 : 0;
            
            PlayMusic();
        }

        private void PlayMusic()
        {
            if (TurnOnMusic == 0)
            {
                _musicController.PlayMusic();
            }
            else
            {
                _musicController.StopMusic();
            }
        }
    }
}
