using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.LevelSelect.ComponentUI
{
    public class AvailableLevelButton : MonoBehaviour
    {
        [SerializeField] private Text _countLevelText;
        
        
        public void Activate(string value)
        {
            gameObject.SetActive(true);
            
            if (_countLevelText != null)
            {
                _countLevelText.text = value;
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}