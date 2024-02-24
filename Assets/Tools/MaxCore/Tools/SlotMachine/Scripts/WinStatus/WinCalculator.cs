using System;
using System.Collections.Generic;
using System.Linq;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Data;
using Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.WinStatus
{
    public class WinCalculator : MonoBehaviour
    {
        private readonly Dictionary<List<SlotSymbolPayType>, SlotSymbolPayType> PayTableMap = new();

        public SlotHandler SlotHandler;
        public WinAnimator WinAnimator;

        public LineRaycaster LineRaycaster;
        public LevelPayInfo LevelPayInfo;
        
        public void Init()
        {
            SlotHandler.OnStopSpin += Calculate;
            PrepareLevelPayInfo();
        }

        private void PrepareLevelPayInfo()
        {
            foreach (var pair in LevelPayInfo.PayTableMap)
            {
                var list = new List<SlotSymbolPayType>
                {
                    pair.Key.symbolLevelType1,
                    pair.Key.symbolLevelType2,
                    pair.Key.symbolLevelType3,
                    pair.Key.symbolLevelType4,
                    pair.Key.symbolLevelType5
                };
                var filteredList = list
                    .Where(s => s != SlotSymbolPayType.Undefined)
                    .ToList();

                PayTableMap.Add(filteredList, pair.Value);
            }
        }

        private void Calculate()
        {
            var takenSymbol = CheckAllLine();
            if (takenSymbol.Count == 0)
            {
                SlotHandler.NotifyLose();
                SlotHandler.NotifyFinishSpin();
                SlotHandler.AudioPlayer.PlayAudioLoseSpin();
                return;
            }

            var winAnimationsList = new List<Tuple<List<SlotSymbol>, LineGroup>>();

            foreach (var symbols in takenSymbol)
            {
                var symbolsToType = symbols.Value.Select(s => s.PaySymbolID).ToList();

                if (PayTableMap.Keys.Any(x => x.SequenceEqual(symbolsToType)))
                {
                    var matchedKey = PayTableMap.Keys.FirstOrDefault(x => x.SequenceEqual(symbolsToType));
                    if (matchedKey != null)
                    {
                        SlotHandler.NotifyWinBonus(PayTableMap[matchedKey]);
                        winAnimationsList.Add(Tuple.Create(new List<SlotSymbol>(symbols.Value), symbols.Key));
                    }
                }
            }

            WinAnimator.PlayWinAnimation(winAnimationsList, 
                () => SlotHandler.NotifyFinishSpin(),
                SlotHandler.AudioPlayer.PlayAudioWinSpin);
        }


        private Dictionary<LineGroup, List<SlotSymbol>> CheckAllLine()
        {
            var winLine = new Dictionary<LineGroup, List<SlotSymbol>>();

            foreach (var lineGroup in LineRaycaster.Lines)
            {
                var lastSymbol = SlotSymbolLevelType.All;
                var takenSymbols = new List<SlotSymbol>();
                var isNotFirst = false;

                foreach (var caster in lineGroup.RayCasters)
                {
                    var symbol = caster.GetSymbol();

                    if (lastSymbol != symbol.SymbolID && isNotFirst)
                        break;

                    isNotFirst = true;
                    lastSymbol = symbol.SymbolID;

                    takenSymbols.Add(symbol);
                }

                if (takenSymbols.Count < 3)
                    continue;

                winLine.Add(lineGroup, takenSymbols);
            }

            return winLine;
        }
    }
}