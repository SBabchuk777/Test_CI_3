using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService.Data
{
    [CreateAssetMenu(fileName = nameof(UIViewData), menuName = "Core/Services/UIView/" + nameof(UIViewData))]
    public class UIViewData : ScriptableObject
    {
        public SerializableDictionary<UIViewType, BaseView> ViewDataMap;
    }
}