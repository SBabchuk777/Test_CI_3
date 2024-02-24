using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
    public class Settings : ScriptableObject
    {
        [SerializeField] private string firebaseRemoteKey = "mrgame_string";

        [SerializeField] private string landingUrl = "https://google.com";

        [SerializeField] private string oneSignalAppID;
        
        [SerializeField] private string privacyPoliceUrl;
        
        [SerializeField] private string termsOfUseUrl;
        
        [SerializeField] private bool isDebug;

        private static Settings _instance;

        private static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<Settings>("Settings");

                return _instance;
            }
        }

        public static string FirebaseKey()
        {
            return Instance.firebaseRemoteKey;
        }

        public static bool IsDebug()
        {
            return Instance.isDebug;
        }
        
        public static string DefaultB1n0m()
        {
            return Instance.landingUrl;
        }
        
        public static string OneSignalAppID()
        {
            return Instance.oneSignalAppID;
        }
        
        public static string PolicyOrTermsUrl(PolicyOrTerms value)
        {
            return value == PolicyOrTerms.PrivacyPolice? Instance.privacyPoliceUrl : Instance.termsOfUseUrl;
        }
    }

    [System.Serializable]
    public enum PolicyOrTerms
    {
        PrivacyPolice,
        TermsOfUse
    }
}
