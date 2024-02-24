using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class BoosterZoneView : MonoBehaviour
    {
        [SerializeField] 
        private Text _firstBoosterCountText;
        [SerializeField] 
        private Text _secondBoosterCountText;
        [SerializeField] 
        private Image _firstCounterImage;
        [SerializeField] 
        private Image _secondCounterImage;
        [SerializeField] 
        private Button _firstBtn;
        [SerializeField] 
        private Button _secondBtn;
        
        public void UpdateFirstBoosterCountText(int value)
        {
            _firstBoosterCountText.text = value.ToString();
        }

        public void UpdateSecondBoosterCountText(int value)
        {
            _secondBoosterCountText.text = value.ToString();
        }

        public void SetActiveBoosters(int numberBooster, bool active)
        {
            switch (numberBooster)
            {
                case 0:
                    SetActiveFirstBooster(active);
                    break;
                case 1:
                    SetActiveSecondBooster(active);
                    break;
            }
        }

        private void SetActiveFirstBooster(bool active)
        {
            if (!active)
            {
                _firstBtn.interactable = false;
                _firstCounterImage.color = _firstBtn.colors.disabledColor;
                _firstBoosterCountText.color = _firstBtn.colors.disabledColor;
            }
            else
            {
                _firstBtn.interactable = true;
                _firstCounterImage.color = _firstBtn.colors.normalColor;
                _firstBoosterCountText.color = _firstBtn.colors.normalColor;
            }
        }
        
        private void SetActiveSecondBooster(bool active)
        {
            if (!active)
            {
                _secondBtn.interactable = false;
                _secondCounterImage.color = _firstBtn.colors.disabledColor;
                _secondBoosterCountText.color = _firstBtn.colors.disabledColor;
            }
            else
            {
                _secondBtn.interactable = true;
                _secondCounterImage.color = _firstBtn.colors.normalColor;
                _secondBoosterCountText.color = _firstBtn.colors.normalColor;
            }
        }
    }
}
