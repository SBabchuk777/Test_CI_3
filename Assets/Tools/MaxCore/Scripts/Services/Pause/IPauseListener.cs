namespace Tools.MaxCore.Scripts.Services.Pause
{
    public interface IPauseListener
    {
        void OnStartPause();
        void OnFinishPause();
    }
}