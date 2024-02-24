using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class BallTeleporter : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _padding;
        private Camera mainCamera;
        private Vector3 ViewportPoint => mainCamera.WorldToViewportPoint(_target.position);
        
        private float screenWidth;
        

        public void Initialize()
        {
            mainCamera = Camera.main;
            screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        }

        private void Update()
        {
            if (ViewportPoint.x is < 0 or > 1)
            {
                Teleport();
            }
        }

        private void Teleport()
        {
            if (_target.position.x - mainCamera.transform.position.x < -screenWidth/2 - _padding)
            {
                _target.position = new Vector3(_target.position.x + screenWidth + (_padding * 2),
                    _target.position.y);
            }
        
            if (_target.position.x - mainCamera.transform.position.x > screenWidth/2 + _padding)
            {
                _target.position = new Vector3(_target.position.x - screenWidth - (_padding * 2),
                    _target.position.y);
            }
        }
    }
}