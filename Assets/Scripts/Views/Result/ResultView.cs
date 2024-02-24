using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Result
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] 
        private List<Image> _starImages;
        [SerializeField] 
        private List<Sprite> _starSprites;
        [SerializeField] 
        private Image _levelResultImage;
        [SerializeField] 
        private List<Sprite> _levelResultSprites;

        public void UpdateImages(int value)
        {
            foreach (var starImage in _starImages)
            {
                starImage.sprite = _starSprites[0];
            }
            
            for (int i = 0; i < value; i++)
            {
                _starImages[i].sprite = _starSprites[1];
            }

            _levelResultImage.sprite = value < 3 ? _levelResultSprites[0] : _levelResultSprites[1];
            _levelResultImage.SetNativeSize();
            _levelResultImage.rectTransform
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _levelResultImage.rectTransform.rect.width/4);
            _levelResultImage.rectTransform
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _levelResultImage.rectTransform.rect.height/4);
        }
    }
}