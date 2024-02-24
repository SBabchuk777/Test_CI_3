using Game.Scripts.Runtime.Feature.Services.CoinAtLevelService;
using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Hoop
{
    public class HoopHandler : MonoBehaviour
    {
        [SerializeField] private HoopBallDetector _topDetector;
        [SerializeField] private HoopBallDetector _goalDetector;
        [SerializeField] private HoopBallDetector _missDetector;

        [SerializeField] private GameObject _backlights;

        private CoinService coinService;
        public bool IsGoal { get; private set; }

        public void Initialize(CoinService coinService)
        {
            this.coinService = coinService;

            _topDetector.OnBallInArea += ActivateDetectors;
            _missDetector.OnBallInArea += CheckMiss;
            _goalDetector.OnBallInArea += NotifyGoal;

            DeactivateDetectors();
        }

        private void ActivateDetectors()
        {
            _goalDetector.ActivateCollider();
            _missDetector.ActivateCollider();
        }

        private void DeactivateDetectors()
        {
            _goalDetector.DeactivateCollider();
            _missDetector.DeactivateCollider();
        }

        private void DeactivateHoop()
        {
            _goalDetector.DeactivateCollider();
            _missDetector.DeactivateCollider();
            _topDetector.DeactivateCollider();
            
            _backlights.SetActive(false);
        }

        private void CheckMiss()
        {
            if (!IsGoal)
            {
                DeactivateDetectors();
            }
        }

        private void NotifyGoal()
        {
            IsGoal = true;
            DeactivateHoop();
            coinService.AddCoin(10);
        }
    }
}