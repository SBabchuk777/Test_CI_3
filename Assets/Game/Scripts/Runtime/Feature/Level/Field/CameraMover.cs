using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Field
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _upT;
        [SerializeField] private float _downT;
        [SerializeField] private float _leftT;
        [SerializeField] private float _rightT;

        [SerializeField] private Transform _moveAnchor;
        [SerializeField] private Transform _playerTransform;

        private void LateUpdate()
        {
            RunUpDownCamera(_playerTransform.position.y > _moveAnchor.transform.position.y ? _upT : _downT);
            RunLeftRightCamera(_playerTransform.position.x > _moveAnchor.transform.position.x ? _rightT : _leftT);
        }

        private void RunUpDownCamera(float t)
        {
            var positionY = transform.position.y + (_playerTransform.position.y - _moveAnchor.position.y);

            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(transform.position.x, positionY, transform.position.z),
                t * Time.deltaTime);
        }
        
        private void RunLeftRightCamera(float t)
        {
            var positionX = transform.position.x + (_playerTransform.position.x - _moveAnchor.position.x);

            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(positionX,transform.position.y, transform.position.z),
                t * Time.deltaTime);
        }
    }
}