using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Views.Bonus;
using Models;

namespace Controllers.SceneControllers
{
    public class BonusController : AbstractController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField] 
        private PrizeZoneView _prizeZoneView;
        [SerializeField] 
        private WheelView _wheelView;
        [Space(5)] [Header("UI")] 
        [SerializeField]
        private Text _coinCountText;
        [SerializeField] 
        private Button _spinBtn;
        [SerializeField] 
        private Button _closeBtn;
        [Space(5)] [Header("AudioClips")] 
        [SerializeField]
        private AudioClip _clickClip;
        [SerializeField] 
        private AudioClip _rotationWheelClip;
        [SerializeField] 
        private AudioClip _winClip;
        [SerializeField] 
        private AudioClip _loseClip;

        private BonusModel _model;

        protected override void OnEnableScene()
        {
            _model = new BonusModel();
            
            CheckLastDay();
            
            UpdateCoinCountText();
            
            _closeBtn.onClick.AddListener(delegate { LoadSceneMenu(true); });
            _spinBtn.onClick.AddListener(StartAnim);
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _closeBtn.onClick.RemoveAllListeners();
            _spinBtn.onClick.RemoveAllListeners();
        }

        private void UpdateCoinCountText()
        {
            _coinCountText.text = CoinCount.ToString();
        }

        private void StartAnim()
        {
            PlaySound(_rotationWheelClip);
            _spinBtn.interactable = false;
            _wheelView.StartRotateWheel();
            _wheelView.RotationEndAction += EndRotation;
        }

        private void EndRotation(int value)
        {
            if (value == -1)
            {
                _spinBtn.interactable = true;
                PlaySound(_loseClip);
            }
            else if(value == 0)
            {
                PlaySound(_loseClip);
                _prizeZoneView.gameObject.SetActive(true);
                _prizeZoneView.UpdateText(value);
            }
            else
            {
                PlaySound(_winClip);
                CoinCount += value;
                _prizeZoneView.gameObject.SetActive(true);
                _prizeZoneView.UpdateText(value);
                UpdateCoinCountText();
            }
        }

        private void LoadSceneMenu(bool isClick)
        {
            _model.LastDayOpen = DateTime.Now.Day;

            if (isClick)
            {
                PlaySound(_clickClip);
            }

            StartCoroutine(DelayLoadScene(isClick));
        }

        private void CheckLastDay()
        {
            if (!_model.CanRotateWheel)
            {
                LoadSceneMenu(false);
            }
        }

        private IEnumerator DelayLoadScene(bool isDelay)
        {
            float delay = isDelay ? 0.3f : 0;
            
            yield return new WaitForSeconds(delay);
            
            SceneManager.LoadScene("Menu");
        }
    }
}