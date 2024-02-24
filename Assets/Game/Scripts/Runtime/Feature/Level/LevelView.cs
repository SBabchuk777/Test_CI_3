using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private LevelArbiter _levelArbiter;

        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(_levelArbiter.OpenPauseView);
        }
    }
}