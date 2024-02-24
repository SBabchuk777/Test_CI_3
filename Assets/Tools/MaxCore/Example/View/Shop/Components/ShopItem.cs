using System;
using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Shop.Components
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private SerializableDictionary<ShopStateType, ShopItemState> _itemStatesMap;

        [SerializeField] private string _nameBuyButton;
        [SerializeField] private string _nameNotCanBuyButton;
        [SerializeField] private string _nameGetButton;
        
        public void SetImage(Sprite sprite)
        {
            _itemImage.sprite = sprite;
        }

        public void SetBuyButton(Action callback)
        {
            _itemStatesMap[ShopStateType.Buy].Activate();
            _itemStatesMap[ShopStateType.Buy].SetButton(() => callback?.Invoke());
        }

        public void SetGetButton(Action callback)
        {
            _itemStatesMap[ShopStateType.Get].Activate();
            _itemStatesMap[ShopStateType.Get].SetButton(() => callback?.Invoke());
        }

        public void SetCountText(string value)
        {
            _itemStatesMap[ShopStateType.Buy].SetButtonText(_nameBuyButton + value);
            _itemStatesMap[ShopStateType.NotCanBuy].SetButtonText(_nameNotCanBuyButton + value);
            _itemStatesMap[ShopStateType.Get].SetButtonText(_nameGetButton);
        }
        
        public void SetNotCanBuy()
        {
            _itemStatesMap[ShopStateType.NotCanBuy].Activate();
        }

        public void SetItemSelected()
        {
            _itemStatesMap[ShopStateType.Selected].Activate();
        }

        public void ResetItems()
        {
            foreach (var key in _itemStatesMap.Keys)
            {
                _itemStatesMap[key].ResetItem();
            }
        }
    }
}