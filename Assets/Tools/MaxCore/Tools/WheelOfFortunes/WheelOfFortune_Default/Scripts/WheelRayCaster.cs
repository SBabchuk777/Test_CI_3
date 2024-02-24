using UnityEngine;

namespace Tools.MaxCore.Tools.WheelOfFortunes.WheelOfFortune_Default.Scripts
{
    public class WheelRayCaster : MonoBehaviour
    {
        public CellType GetCellType()
        {
            var hit = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y));
            return hit ? hit.GetComponent<WheelCell>().CellType : CellType.Undefined;
        }
    }
}