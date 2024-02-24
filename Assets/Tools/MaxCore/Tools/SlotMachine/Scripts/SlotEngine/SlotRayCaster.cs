using System.Linq;
using UnityEngine;

namespace Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine
{
    public class SlotRayCaster : MonoBehaviour
    {
        public SlotSymbol GetSymbol()
        {
            var hit = Physics2D.OverlapPointAll(new Vector2(transform.position.x, transform.position.y)).FirstOrDefault(h => h.GetComponent<SlotSymbol>());
            return hit ? hit.GetComponent<SlotSymbol>() : null;
        }
    }
}