using System;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.TimerService.Converter
{
    [Serializable]
    public class TimeInput
    {
        [SerializeField] private float hours;
        [SerializeField] private float minutes;
        [SerializeField] private float seconds;

        public float Hours
        {
            get => hours;
            set => hours = Mathf.Clamp(value, 0f, 24f);
        }

        public float Minutes
        {
            get => minutes;
            set => minutes = Mathf.Clamp(value, 0f, 60f);
        }

        public float Seconds
        {
            get => seconds;
            set => seconds = Mathf.Clamp(value, 0f, 60f);
        }


        public void Reset()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }
    }
}