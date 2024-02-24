using System;
using UnityEngine;

namespace Models
{
    public class BonusModel
    {
        private const string LastDateOpenName = "LastDateOpen";

        public bool CanRotateWheel
        {
            get
            {
                if (!PlayerPrefs.HasKey(LastDateOpenName))
                {
                    return true;
                }
                else
                {
                    int lastDay = PlayerPrefs.GetInt(LastDateOpenName);

                    return lastDay != DateTime.Now.Day;
                }
            }
        }

        public int LastDayOpen
        {
            set => PlayerPrefs.SetInt(LastDateOpenName, value);
        }
    }
}
