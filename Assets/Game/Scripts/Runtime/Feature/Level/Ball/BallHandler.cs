using System;
using System.Collections.Generic;
using Game.Scripts.Runtime.Feature.Level.Ball;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.Pause;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class BallHandler : MonoBehaviour, IPauseListener
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _skin;

        [SerializeField] private BallTeleporter _teleporter;
        [SerializeField] private BallTouchInput _touchInput;
        [SerializeField] private BallMover _mover;

        [SerializeField] private MoveEffect _effectPrefab;
        [SerializeField] private Transform _effectsAnchor;
        [Header("Settings")] [SerializeField] private float _forceMove;
        [SerializeField] private float _forceMoveX;
        [SerializeField] private float _deltaYpos;

        [Inject] private PauseService pauseService;
        [Inject] private ProjectAudioPlayer audioPlayer;


        private List<MoveEffect> effects = new List<MoveEffect>();
        private bool isCanMove;
        private bool isDefeat;
        private float lastYpos;

        public void Initialize()
        {
            _teleporter.Initialize();
            _touchInput.OnTouch += Move;
            pauseService.RegisterListener(this);
        }

        private void OnDestroy()
        {
            pauseService.UnregisterListener(this);
        }

        private void Update()
        {
            CheckMove();

            if (isCanMove)
            {
                _rigidbody2D.velocity = new Vector2(_mover.tiltX * _forceMoveX, _rigidbody2D.velocity.y);
                _mover.MoveWithAcceleration();
            }
        }

        private void CheckMove()
        {
            if (!isCanMove && _rigidbody2D.velocity.y != 0)
            {
                isCanMove = true;
            }

            if (isCanMove && Mathf.Abs(lastYpos - transform.position.y) <= _deltaYpos)
            {
                isCanMove = false;
            }

            lastYpos = transform.position.y;
        }

        private void Move()
        {
            ActivateMoveEffect();

            _rigidbody2D.AddForce((Vector2.up) * _forceMove, ForceMode2D.Impulse);
            audioPlayer.PlayAudioSfx(ProjectAudioType.PlayerMove);
        }

        public void ActivateMoveEffect()
        {
            var effect = Instantiate(_effectPrefab, _effectsAnchor.position, Quaternion.identity, _effectsAnchor);
            effect.Initialize(() => effects.Remove(effect));
            effects.Add(effect);
        }

        public void Defeat()
        {
            isCanMove = false;

            _rigidbody2D.Sleep();
            _rigidbody2D.simulated = false;

            gameObject.SetActive(false);
        }

        public void Respawn(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;

            gameObject.SetActive(true);

            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.WakeUp();
            _rigidbody2D.simulated = true;
        }

        public void OnStartPause()
        {
            _rigidbody2D.Sleep();
            _rigidbody2D.simulated = false;

            effects.ForEach(e => e.Stop());
        }

        public void OnFinishPause()
        {
            _rigidbody2D.WakeUp();
            _rigidbody2D.simulated = true;

            effects.ForEach(e => e.Run());
        }

        public void SetSkin(Sprite ballSkin)
        {
            _skin.sprite = ballSkin;
        }
    }
}