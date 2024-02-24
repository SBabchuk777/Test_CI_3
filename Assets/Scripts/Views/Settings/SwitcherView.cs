using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Settings
{
    public class SwitcherView : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;
        [SerializeField] 
        private List<Sprite> _sprites;

        public void UpdateSwitcherSprite(int value)
        {
            _image.sprite = _sprites[value];
        }
    }
}
