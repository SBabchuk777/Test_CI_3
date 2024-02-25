using System.Collections.Generic;
using Game.Scripts.Runtime.Feature.Level.Common;
using Game.Scripts.Runtime.Feature.Level.Field;
using Game.Scripts.Runtime.Feature.Player;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.DataHubService;
using Tools.MaxCore.Scripts.Services.DataHubService.Data;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private LevelHandler _levelHandler;
        [SerializeField] private FieldHandler _fieldHandler;
        [SerializeField] private BallHandler _ballHandler;

        [SerializeField] private Vector3 _yOffsetSpawnPosition;
        [SerializeField] private List<Sprite> _ballSkins;

        [Inject] private DataHub dataHub;
        
        public void Initialize()
        {
            _fieldHandler.Initialize();
            
            _ballHandler.Initialize();
            _ballHandler.SetSkin(_ballSkins[dataHub.LoadData<PlayerProgressData>(DataType.Progress).CurrentIDItem]);

            _levelHandler.OnLose += _ballHandler.Defeat;
        }

        public void ContinueLevel()
        {
            var spawnPosition = _fieldHandler.GetLastPlatform() + _yOffsetSpawnPosition;

            _ballHandler.Respawn(spawnPosition);
            
            _fieldHandler.DeactivateHoopsUnderPlayer();
            _levelHandler.Reset();
        }
    }
}