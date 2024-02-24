using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Example.View.LevelSelect.ComponentUI
{
    public class StarsPanel : MonoBehaviour
    {
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        
        [SerializeField] private List<Image> stars;

        public void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public void SetActiveStars(int countStar)
        {
            for (var i = 0; i < stars.Count; i++)
            {
                stars[i].sprite = i < countStar ? activeSprite : inactiveSprite;
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}