using System.Collections;
using System.Collections.Generic;
using Models.Game;
using Tools.UnityAdsService.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.SceneControllers
{
    public class ShopController : AbstractController
    {
        [Space(5)] [Header("UI")] 
        [SerializeField]
        private Text _coinCountText;
        [SerializeField] 
        private Button _backBtn;
        [SerializeField] 
        private Button _buyFirstBoostBtn;
        [SerializeField] 
        private Button _buySecondBoostBtn;
        [Space(5)] [Header("AdButtons")] 
        [SerializeField]
        private List<UnityAdsButton> _adButtons;
        [Space(5)] [Header("AudioClips")] 
        [SerializeField]
        private AudioClip _clickClip;

        private GameModel _model;
        
        protected override void OnEnableScene()
        {
            _model = new GameModel();
            
            SetCoinsText();
            CheckActiveBuyBtns();
            
            _backBtn.onClick.AddListener(LoadSceneMenu);
            _buyFirstBoostBtn.onClick.AddListener(PressBuyFirstBoostBtn);
            _buySecondBoostBtn.onClick.AddListener(PressBuySecondBoostBtn);

            _adButtons[0].OnCanGetReward += GetReward;
            _adButtons[1].OnCanGetReward += GetReward;
            _adButtons[2].OnCanGetReward += GetReward;
        }

        protected override void OnStartScene()
        {
            
        }

        protected override void OnDisableScene()
        {
            _backBtn.onClick.RemoveAllListeners();
            _buyFirstBoostBtn.onClick.RemoveAllListeners();
            _buySecondBoostBtn.onClick.RemoveAllListeners();
            
            _adButtons[0].OnCanGetReward -= GetReward;
            _adButtons[1].OnCanGetReward -= GetReward;
            _adButtons[2].OnCanGetReward -= GetReward;
        }

        private void SetCoinsText()
        {
            _coinCountText.text = CoinCount.ToString();
        }

        private void PressBuyFirstBoostBtn()
        {
            SetClickClip();
            
            _model.SameCellsBoosterCount++;
            CoinCount -= 100;
            CheckActiveBuyBtns();
            SetCoinsText();
        }
        
        private void PressBuySecondBoostBtn()
        {
            SetClickClip();
            
            _model.OneColumnBoosterCount++;
            CoinCount -= 100;
            CheckActiveBuyBtns();
            SetCoinsText();
        }

        private void CheckActiveBuyBtns()
        {
            _buyFirstBoostBtn.interactable = CoinCount >= 100;
            _buySecondBoostBtn.interactable = CoinCount >= 100;
        }

        private void GetReward(int value)
        {
            CoinCount += value * 500;
            
            SetCoinsText();
            CheckActiveBuyBtns();
        }

        private void SetClickClip()
        {
            PlaySound(_clickClip);
        }

        private void LoadSceneMenu()
        {
            SetClickClip();

            StartCoroutine(DelayLoadScene());
        }
        
        private IEnumerator DelayLoadScene()
        {
            yield return new WaitForSeconds(0.3f);
            
            SceneManager.LoadScene("Menu");
        }
    }
}
