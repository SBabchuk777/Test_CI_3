using System;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.TimerService.Converter
{
    public class TimeConverter
    {
        public string FormatSecondsToMMSS(float timeRemaining)
        {
            if (timeRemaining > 3600)
                Debug.LogException(new Exception("Time remaining exceeds one hour."));

            var minutes = Mathf.FloorToInt(timeRemaining / 60);
            var seconds = Mathf.FloorToInt(timeRemaining % 60);

            return $"{minutes:00}:{seconds:00}";
        }

        public string FormatSecondsToHHMMSS(float timeRemaining)
        {
            if (timeRemaining > 86400)
                throw new Exception("Time remaining exceeds one day.");

            var hours = Mathf.FloorToInt(timeRemaining / 3600);
            var minutes = Mathf.FloorToInt((timeRemaining % 3600) / 60);
            var seconds = Mathf.FloorToInt(timeRemaining % 60);

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        public float FormatHHMMSSToSeconds(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
                throw new ArgumentException("Invalid time string.");

            var parts = timeString.Split(':');

            if (parts.Length != 3)
                throw new ArgumentException("Invalid time string format.");

            if (!int.TryParse(parts[0], out var hours) ||
                !int.TryParse(parts[1], out var minutes) ||
                !int.TryParse(parts[2], out var seconds))
                throw new ArgumentException("Invalid time string format.");

            if (hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentException("Invalid time values.");

            float totalSeconds = hours * 3600 + minutes * 60 + seconds;
            return totalSeconds;
        }

        public float FormatMMSSToSeconds(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
                throw new ArgumentException("Invalid time string.");

            string[] parts = timeString.Split(':');

            if (parts.Length != 2)
                throw new ArgumentException("Invalid time string format.");

            if (!int.TryParse(parts[1], out var minutes) ||
                !int.TryParse(parts[2], out var seconds))
                throw new ArgumentException("Invalid time string format.");

            if (minutes < 0 || seconds < 0)
                throw new ArgumentException("Invalid time values.");

            float totalSeconds = minutes * 60 + seconds;
            return totalSeconds;
        }

        public float TimeInputToSeconds(TimeInput timeInput)
        {
            return timeInput.Hours * 3600 + timeInput.Minutes * 60 + timeInput.Seconds;
        }
        public TimeInput SecondsToTimeInput(float totalSeconds)
        {
            return new TimeInput
            {
                Hours = Mathf.FloorToInt(totalSeconds / 3600),
                Minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60),
                Seconds = Mathf.FloorToInt(totalSeconds % 60)
            };
        }
    }
}