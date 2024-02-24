using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Shop.Components
{
    public class ShopItemState : MonoBehaviour
    {
        [SerializeField] private Text _buttonText;
        [SerializeField] private Button _button;
        [SerializeField] private ShopStateType _stateType;
        
        public void SetButton(Action callback)
        {
            _button.onClick.AddListener(() => callback?.Invoke());
        }
        public void SetButtonText(string value)
        {
            _buttonText.text = value;
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);

            if (_stateType is ShopStateType.Buy or ShopStateType.Get)
            {
                _button.gameObject.SetActive(true);
                _button.interactable = true;
            }
        }
    
        public void ResetItem()
        {
            if (_stateType is ShopStateType.Buy or ShopStateType.Get)
            {
                _button.onClick.RemoveAllListeners();
                _button.gameObject.SetActive(false);
            }
            
            gameObject.SetActive(false);
        }
    }
}