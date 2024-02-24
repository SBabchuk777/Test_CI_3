using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UIViews.Slot
{
    public class ExampleInformationPanel : MonoBehaviour
    {
        [SerializeField] private Text _panelText;
        [SerializeField] private Text _countAttempts;
        [SerializeField] private Text _countAttemptsText;
        

        public void SetText(string value)
        {
            _panelText.text = value;
        }
        
        public void SetCountText(string value)
        {
            _countAttempts.text = $" {value}";
        }
    }
}