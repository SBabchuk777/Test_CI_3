using System;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    [Serializable]
    public class SlotControllerSettings
    {
        public float MoveSpeed;
        public float StepIncreaseRate;

        public float TimeSpin;
        public float AwaitTimeForStartGroup;
        public float AwaitTimeStopSpin;
    }
}