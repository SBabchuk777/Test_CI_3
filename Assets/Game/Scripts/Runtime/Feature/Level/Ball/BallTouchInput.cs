using System;
using Game.Scripts.Runtime.Services.SateMachine;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.Pause;
using Tools.MaxCore.Scripts.Services.SateMachine;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Ball
{
    public class BallTouchInput : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _touchArea;
        public event Action OnTouch;

        [Inject] private GameStateMachine stateMachine;
        [Inject] private PauseService pauseService;
        
        private Vector3 TouchWorldPosition => 
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        private bool IsTouchArea =>
            _touchArea.bounds.Contains(new Vector3(TouchWorldPosition.x, TouchWorldPosition.y, _touchArea.transform.position.z));
        
        private void Update()
        {
            if (stateMachine.LevelState != LevelState.Game || pauseService.IsPaused)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0) && IsTouchArea)
            {
                OnTouch?.Invoke();
            }
        }
    }
}