using UnityEngine;

namespace Core
{
    public static class Debugger
    {
        private static bool _isLogging;
        
        public static void Log(object message)
        {
            if(Settings.IsDebug())
                Debug.Log(message);
        }
        
        public static void LogError(object message)
        {
            if(Settings.IsDebug())
                Debug.LogError(message);
        }
        
        public static void LogWarning(object message)
        {
            if(Settings.IsDebug())
                Debug.LogWarning(message);
        }
    }
}

