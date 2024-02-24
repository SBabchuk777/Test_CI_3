using System;
using System.Linq;
using Tools.MaxCore.Scripts.Project.Audio;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Tools.SlotMachine.Scripts.SlotEngine;
using UnityEngine;

namespace Tools.MaxCore.Scripts.ComponentHelp
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Sprite _activeButton;
        [SerializeField] private Sprite _inactiveButton;

        private ProjectAudioPlayer ProjectAudioPlayer => ProjectContext.Instance.GetDependence<ProjectAudioPlayer>();
        public event Action OnClick;

        public void OnMouseUp()
        {
            if (ProjectAudioPlayer != null)
                ProjectAudioPlayer.PlayAudioSfx(ProjectAudioType.Click);

            OnClick?.Invoke();
        }

        public void Deactivate()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _inactiveButton;
            }

            _collider2D.enabled = false;
        }

        public void Activate()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _activeButton;
            }

            _collider2D.enabled = true;
        }
    }
}