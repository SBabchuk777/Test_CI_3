using System;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;

namespace Tools.MaxCore.Scripts.ComponentHelp
{
    public class ButtonHoldAction : MonoBehaviour
    {
        [SerializeField] private float holdTime;
    
        private float clickTime;

        private ProjectAudioPlayer ProjectAudioPlayer => ProjectContext.Instance.GetDependence<ProjectAudioPlayer>();
        public event Action OnClick;
        public event Action OnButtonHold;
        

        private void OnMouseDown() => 
            clickTime = Time.time;

        private void OnMouseUp()
        {
            if (Time.time - clickTime < holdTime)
                OnClick?.Invoke();
            else
                OnButtonHold?.Invoke();

            if (ProjectAudioPlayer != null) 
                ProjectAudioPlayer.PlayAudioSfx(ProjectAudioType.Click);
        }
    }
}