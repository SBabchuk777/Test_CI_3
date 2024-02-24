using UnityEngine;
using UnityEngine.UI;

namespace Views.Bonus
{
    public class PrizeZoneView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateText(int value)
        {
            _text.text = "+" + value;
        }
    }
}