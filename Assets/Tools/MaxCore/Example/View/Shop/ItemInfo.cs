using System;
using UnityEngine;

namespace Tools.MaxCore.Example.View.Shop
{
    [Serializable]
    public class ItemInfo
    {
        public Sprite Skin;
        public int Count;
        public string Name;

        public int ID { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsCanPurchased { get; set; }
        public bool IsSelected { get; set; }
    }
}