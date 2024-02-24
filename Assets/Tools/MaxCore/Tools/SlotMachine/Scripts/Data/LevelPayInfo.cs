using Tools.MaxCore.Tools.SerializableComponent;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.Data
{
    [CreateAssetMenu(menuName = "Core/Tools/SlotMachine/Path/" + nameof(LevelPayInfo), fileName = nameof(LevelPayInfo))]
    public class LevelPayInfo : ScriptableObject
    {
        public SerializableDictionary<LineCombination, SlotSymbolPayType> PayTableMap;
    }
}