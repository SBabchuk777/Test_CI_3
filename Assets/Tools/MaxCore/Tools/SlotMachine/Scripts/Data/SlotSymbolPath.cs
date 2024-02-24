using Tools.MaxCore.Tools.SerializableComponent;
using Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.Data
{
    [CreateAssetMenu(menuName = "Core/Tools/SlotMachine/Path/" + nameof(SlotSymbolPath), fileName = nameof(SlotSymbolPath))]
    public class SlotSymbolPath : ScriptableObject
    {
        public SerializableDictionary<SlotSymbolLevelType, SlotSymbol> PathMap;
    }
}