using Tools.MaxCore.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.LevelSelect.ComponentUI
{
    public class ClosedLevelButton : MonoBehaviour
    {
        [SerializeField] private Text _countLevelText;
        public void Activate(string value)
        {
            gameObject.SetActive(true);

            if (_countLevelText != null)
            {
                _countLevelText.SetAlpha(0.7f);
                _countLevelText.text = value;
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}