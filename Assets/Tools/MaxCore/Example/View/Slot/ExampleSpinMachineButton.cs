using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.Runtime.Feature.UIViews.Slot
{
    public class ExampleSpinMachineButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        public bool Interactable
        {
            set => _button.interactable = value;
        }

        public void SetSpin(UnityAction callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(callback);
        }
        
        public void SetPlay(UnityAction callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(callback);
            
            _button.interactable = true;
            
            _text.text = "Play";
        }
        
        public void SetBack(UnityAction callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(callback);
            _text.text = "Back";
        }

        public void RemoveAllListeners()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}