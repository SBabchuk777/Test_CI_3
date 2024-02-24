using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.Data
{
    [Serializable]
    public class LineCombination
    {
       public SlotSymbolPayType symbolLevelType1;
       public SlotSymbolPayType symbolLevelType2;
       public SlotSymbolPayType symbolLevelType3;
       public SlotSymbolPayType symbolLevelType4;
       public SlotSymbolPayType symbolLevelType5;
    }
    [Serializable]
    public class LineCombination1
    {
        [field: SerializeField] public List<SlotSymbolPayType> symbolLevelType;
        
    }
    
}