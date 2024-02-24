using System;

namespace Tools.MaxCore.Scripts.Services.Audio.AudioCore
{
    public class AudioServiceException : ApplicationException
    {
        public AudioServiceException(string message) : base(message)
        {
        }
    }
}