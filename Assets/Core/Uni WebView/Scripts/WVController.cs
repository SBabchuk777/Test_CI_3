using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class WVController : MonoBehaviour
    {
        public const string AGREE_TERMS = "Agree_Terms";

        [SerializeField, Header("Ignore errors")]
        private bool _ignoreErrors = true;

        [SerializeField, Header("Config")]
        private RemoteConfig config;

        [SerializeField, Header("RectTransform")]
        private RectTransform _reference;

        [SerializeField, Header("RectTransform 2")]
        private RectTransform _reference2;

        [SerializeField, Header("Background")]
        private GameObject _bgWebView;

        private AgreeTerms _bttnAgreeTerms;

        [Space(10)] [SerializeField] private UnityEvent WebViewLoadingError;

        [SerializeField] private UnityEvent WVLoadingCompleted;

        [SerializeField] private UnityEvent DefaultB1n0mLoadingCompleted;

        private bool is0ffer;

        private UniWebView _UWV;

        private string urlB1n0m;

        private bool isVisible;

        public void Initialize()
        {
            Debugger.Log("@@@ WV Initialize");

            GetAgreeTerms().Init();

            if (_UWV)
            {
                Debugger.Log("@@@ WV Initialize -> LoadUrl");
                LoadUrl();
            }
            else
            {
#if UNITY_EDITOR
                DefaultB1n0mLoadingCompleted?.Invoke();
#else
                if (config != null)
                {
                    config.InitConfig(CheckUrl);
                }
                else
                {
                    DefaultB1n0mLoadingCompleted?.Invoke();
                }
#endif
            }
        }

        public void Back()
        {
            if (_UWV)
            {
                _UWV.GoBack();
            }
        }

        private void CheckUrl()
        {
            Debugger.Log("@@@ CheckUrl");

            urlB1n0m = config.GetURL();

            Debugger.Log($"@@@ urlB1n0m: {urlB1n0m}");

            if (String.IsNullOrEmpty(urlB1n0m))
            {
                DefaultB1n0mLoadingCompleted?.Invoke();

                return;
            }

            Debugger.Log("@@@ CheckUrl -> LoadUrl");
            
            LoadUrl();
        }

        private void CreateWV()
        {
            var WVGO = new GameObject("UWV");

            _UWV = WVGO.AddComponent<UniWebView>();

            _UWV.OnShouldClose += (view) => false;
        }

        private void SetRectWV()
        {
            if (_reference)
            {
                _UWV.ReferenceRectTransform = is0ffer ? _reference2 : _reference;
            }
            else
            {
                _UWV.Frame = new Rect(0, 0, Screen.width, Screen.height);
            }
        }

        private void LoadUrl()
        {
            Debugger.Log($"@@@ LoadUrl:: _UWV ?= null:{_UWV == null}");
           
            if (_UWV == null)
            {
                CreateWV();
            }

            _UWV.Load(urlB1n0m);

            Subscribe();
        }

        private void OnPageErrorReceived(UniWebView view, int statusCode, string url)
        {
            Debugger.Log("xxxxx OnPageErrorReceived statusCode=" + statusCode);

            if (_ignoreErrors)
                return;

            switch (statusCode)
            {
                case -10:
                case -1202:
                    return;
            }

            UnSubscribe();

            HideWebView();
        }
     
        private void OnPageFinished(UniWebView view, int statusCode, string url)
        {
            Debugger.Log($"@@@ OnPageFinished: {url}");

            if (url != "about:blank")
            {
                urlB1n0m = _UWV.Url;
            }

            if (config.isUrlDefaultB1n0m(url))
            {
                Debugger.Log($"@@@ isUrlDefaultB1n0m: {url}");
                
                CheckAgreeTerms();
            }
            else
            {
                Debugger.Log($"@@@ NO isUrlDefaultB1n0m: {url}");

                is0ffer = true;

                ShowWebView();
            }
        }
        
        private void Subscribe()
        {
            _UWV.OnPageFinished += OnPageFinished;
            _UWV.OnPageErrorReceived += OnPageErrorReceived;
        }
        
        private void UnSubscribe()
        {
            _UWV.OnPageFinished -= OnPageFinished;
            _UWV.OnPageErrorReceived -= OnPageErrorReceived;
        }

        private void CheckAgreeTerms()
        {
            Debugger.Log($"@@@ HasKey: {PlayerPrefs.HasKey(AGREE_TERMS)}");
            
            if (PlayerPrefs.HasKey(AGREE_TERMS))
            {
                RemoveWebView();
            }
            else
            {
                is0ffer = false;

                ShowWebView();
            }
        }
        
        private void RemoveWebView()
        {
            ShowBackgroundWebView(false);

            DefaultB1n0mLoadingCompleted?.Invoke();

            Destroy(_UWV);

            _UWV = null;
        }

        public void SetAgreeTerms()
        {
            if(_bttnAgreeTerms)
                _bttnAgreeTerms.Set();

            RemoveWebView();
        }

        private void ShowWebView()
        {
            Debug.Log($"is0ffer: {is0ffer}");
            if (_UWV == null)
            {
                return;
            }

            if (isVisible)
            {
                return;
            }

            isVisible = true;

            ShowBackgroundWebView(true);

            SetRectWV();

            _UWV.Show();
            
            if (!is0ffer)
                GetAgreeTerms().SetInteractable(true);

            WVLoadingCompleted?.Invoke();

            if (is0ffer)
                SetAutoRotation();
        }
        
        public void HideWebView()
        {
            GetAgreeTerms().SetInteractable(false);
            
            if (_UWV == null)
            {
                return;
            }

            if (!isVisible)
            {
                return;
            }

            isVisible = false;

            Debugger.Log(" --- Hided");

            ShowBackgroundWebView(false);
            
            _UWV.Hide();
            
            WebViewLoadingError?.Invoke();
        }
        
        private void SetAutoRotation()
        {
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private void ShowBackgroundWebView(bool value)
        {
            if (_bgWebView)
            {
                _bgWebView.SetActive(value);
            }
        }
        
        private AgreeTerms GetAgreeTerms()
        {
            if(!_bttnAgreeTerms)
            {
                _bttnAgreeTerms = GetComponentInChildren<AgreeTerms>();
            }

            return _bttnAgreeTerms;
        }
    }
}
