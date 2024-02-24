using System;
using System.Collections.Generic;
using System.Linq;
using Tools.MaxCore.Tools.SlotMachine.Scripts.Data;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    public class SlotGroup : MonoBehaviour
    {
        public List<SlotsLine> Lines;
        public float CreateStep;

        public List<SlotSymbolLevelType> SlotsInGroup;
        public List<SlotLineMover> LineMovers;
        
        public Transform StopAnchor;
        public Transform StartAnchor;

        private SlotFactory factory;
        
        private float moveSpeed;
        private float increaseRate;
        private bool isMove;
        private int countCheck;
        
        private float StepForNextLine
            =>   CreateStep * SlotsInGroup.Count;

        public event Action OnStopGroup;
        
        public void Init(SlotSymbolPath slotSymbolPath, SlotControllerSettings settings)
        {
            moveSpeed = settings.MoveSpeed;
            increaseRate = settings.StepIncreaseRate;
            
            InitFactory(slotSymbolPath);
            CreateLine();
        }

        public void MoveGroup() => 
            isMove = true;

        public void StopGroup()
        {
            isMove = false;
            MoveToCell();
        }

        private void CreateLine()
        {
            var createPosition = new Vector3(0, StartAnchor.position.y, 0);

            foreach (var line in Lines)
            {
                line.CreateGroupLine(factory, SlotsInGroup, CreateStep);
                line.transform.position += createPosition;
                
                createPosition += new Vector3(0, StepForNextLine, 0);
            }
        }

        private void FixedUpdate()
        {
            if (!isMove)
                return;
            
            foreach (var mover in LineMovers)
            {
                mover.Move(moveSpeed, increaseRate);

                if (mover.transform.position.y <= StartAnchor.position.y - StepForNextLine )
                {
                    var linePosition = mover.transform.position;
                    mover.transform.position += new Vector3(0, StepForNextLine * 2, 0);
                }
            }
        }

        private void MoveToCell()
        {
            var slotsSymbol = new List<SlotSymbol>();

            foreach (var line in Lines) 
                slotsSymbol.AddRange(line.SlotSymbols);
            
            var nearestSlotSymbol = slotsSymbol
                .Where(s => s.transform.position.y - StopAnchor.position.y < 0)
                .OrderBy(s => Vector3.Distance(s.transform.position, StopAnchor.position))
                .FirstOrDefault();

            FitLineGroup();

            foreach (var mover in LineMovers)
                mover.MoveTo(Vector3.Distance(nearestSlotSymbol.transform.position, StopAnchor.position), CheckOnStop);

        }

        private void FitLineGroup()
        {
            var a = LineMovers.First(l => l.transform.position.y > StartAnchor.position.y);
            var b = LineMovers.First(l => l.transform.position.y < StartAnchor.position.y);

            a.transform.position = b.transform.position + new Vector3(0, StepForNextLine, 0);
        }

        private void CheckOnStop()
        {
            
            if (countCheck++ < LineMovers.Count-1)
                return;
            countCheck = 0;
            
            OnStopGroup?.Invoke();
        }

        private void InitFactory(SlotSymbolPath slotSymbolPath)
        {
            factory = new SlotFactory();
            factory.Init(slotSymbolPath);
        }
    }
}