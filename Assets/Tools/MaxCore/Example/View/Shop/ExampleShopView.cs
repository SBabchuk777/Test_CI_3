using System.Collections.Generic;
using Tools.MaxCore.Example.View.Shop.Components;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.Shop
{
    public class ExampleShopView : BaseView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] List<ShopItem> _shopItems;

        [Inject] private ExampleShopController shopController;

        protected override void Initialize()
        {
            shopController.PrepareItemInfoMap();
            PreparePanel();
        }

        protected override void Subscribe()
        {
            _closeButton.onClick.AddListener(ClosePanel);
            shopController.OnChangePanel += UpdatePanel;
        }

        protected override void Open()
        {
        }

        protected override void Unsubscribe()
        {
            _closeButton.onClick.RemoveAllListeners();
            shopController.OnChangePanel -= UpdatePanel;
        }

        private void PreparePanel()
        {
            for (var i = 0; i < _shopItems.Count; i++)
            {
                var itemInfo = shopController.CurrentItemInfo[i];

                _shopItems[i].SetImage(itemInfo.Skin);
                _shopItems[i].SetCountText(itemInfo.Count.ToString());

                PrepareShopItems(itemInfo, i);
            }
        }

        private void PrepareShopItems(ItemInfo itemInfo, int i)
        {
            _shopItems[i].ResetItems();

            if (itemInfo.IsSelected)
            {
                _shopItems[i].SetItemSelected();
                return;
            }

            if (itemInfo.IsPurchased)
            {
                _shopItems[i].SetGetButton(() => shopController.SetCurrentItem(itemInfo.ID));
                return;
            }

            if (itemInfo.IsCanPurchased)
            {
                _shopItems[i].SetBuyButton(() => shopController.PurchaseItem(itemInfo.ID));
            }
            else
            {
                _shopItems[i].SetNotCanBuy();
            }
        }

        private void UpdatePanel()
        {
            for (var i = 0; i < _shopItems.Count; i++)
            {
                var itemInfo = shopController.CurrentItemInfo[i];
                PrepareShopItems(itemInfo, i);
            }
        }
    }
}