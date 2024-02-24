using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.Pause;
using Tools.MaxCore.Scripts.Services.SateMachine;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Ball
{
    public class BallMover : MonoBehaviour
    {
        private const float MinAccelerationAction = 0.15f;

        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
        
        [Inject] private GameStateMachine stateMachine;
        [Inject] private PauseService pauseService;

        public float tiltX;
        
        public void MoveWithAcceleration()
        {
            if (stateMachine.LevelState != LevelState.Game || pauseService.IsPaused)
            {
                return;
            }
            
            tiltX = Input.acceleration.x;
            
        }
    }
}