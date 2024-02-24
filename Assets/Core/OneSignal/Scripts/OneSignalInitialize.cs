using OneSignalSDK;
using UnityEngine;

namespace Core
{
    public class OneSignalInitialize : MonoBehaviour
    {
        private void Start () 
        {
#if UNITY_IOS
            OneSignal.Default.Initialize(Settings.OneSignalAppID());
        
            OneSignal.Default.PromptForPushNotificationsWithUserResponse();
#else
            Debug.LogError($"Initializing error, OS != IOS");
#endif
        }
    }
}
