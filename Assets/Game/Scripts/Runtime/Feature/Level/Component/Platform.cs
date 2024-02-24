using UnityEngine;

namespace Game.Scripts.Runtime.Feature.Level.Component
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Renderer _spriteRenderer;

        public bool IsVisible => _spriteRenderer.isVisible;

    }
}