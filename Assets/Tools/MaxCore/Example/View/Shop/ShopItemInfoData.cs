using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Example.View.Shop
{
    [CreateAssetMenu(menuName = "Game/Path/" + nameof(ShopItemInfoData), fileName = nameof(ShopItemInfoData))]
    public class ShopItemInfoData : ScriptableObject
    {
        public SerializableDictionary<int, ItemInfo> PathMap;
    }
}