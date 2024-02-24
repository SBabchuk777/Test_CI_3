using Game.Scripts.Runtime.Services.SateMachine;

namespace Tools.MaxCore.Scripts.Services.SateMachine
{
    public class StateInfo
    {
        public LevelState PreviousState { get; }
        public LevelState NewState { get; }

        public StateInfo(LevelState previousState, LevelState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}