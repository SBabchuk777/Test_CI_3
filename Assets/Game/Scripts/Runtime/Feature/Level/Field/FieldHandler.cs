using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Runtime.Feature.Services.CoinAtLevelService;
using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.SateMachine;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Field
{
    public class FieldHandler : MonoBehaviour
    {
        [SerializeField] private CoinService _coinService;
        [SerializeField] private CameraMover _cameraMover;

        [SerializeField] private List<LevelTemplate> _levelTemplates;
        [SerializeField] private List<LevelTemplate> _templateOnLevel;

        [SerializeField] private Transform _parentTemplate;
        [SerializeField] private Transform _playerTransform;

        [Inject] private GameStateMachine gameStateMachine;

        private float screenHeight;
        private bool isMove;
        private Camera mainCamera;

        public void Initialize()
        {
            mainCamera = Camera.main;
            screenHeight = mainCamera.orthographicSize * 2f;

            _templateOnLevel.ForEach(t => t.Initialize(_coinService));

            CreateNewLevel(transform.position.y + screenHeight);
        }

        public Vector3 GetLastPlatform()
        {
            return _templateOnLevel
                .SelectMany(l => l.Platforms)
                .Where(p => p.IsVisible)
                .OrderBy(p => Vector2.Distance(_playerTransform.position, p.transform.position))
                .FirstOrDefault()!.transform.position;
        }
        
        public void DeactivateHoopsUnderPlayer()
        {
            _templateOnLevel
                .SelectMany(l => l.Hoops)
                .Where(h => h.transform.position.y < _playerTransform.position.y)
                .ToList()
                .ForEach(h => h.gameObject.SetActive(false));
        }

        private void Update()
        {
            if (gameStateMachine.LevelState == LevelState.Pause)
            {
                return;
            }

            for (var i = 0; i < _templateOnLevel.Count; i++)
            {
                var template = _templateOnLevel[i];

                if (template.transform.position.y - mainCamera.transform.position.y <= -screenHeight)
                {
                    _templateOnLevel.Remove(template);
                    Destroy(template.gameObject);

                    CreateNewLevel(template.transform.position.y + (screenHeight * 2));
                }
            }
        }

        private void CreateNewLevel(float positionY)
        {
            var randomTemplate = _levelTemplates[Random.Range(0, _levelTemplates.Count)];
            var instance = Instantiate(randomTemplate, new Vector3(0f, positionY, 0f), Quaternion.identity,
                _parentTemplate);
            instance.Initialize(_coinService);

            _templateOnLevel.Add(instance);
        }

        
    }
}