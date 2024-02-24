using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.Services.ResourceVaultService
{
    public class ResourcePanelView : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Text _resourceText;

        private ResourceVault resourceVault;
        
        private void Start()
        {
            resourceVault = ProjectContext.Instance.GetDependence<ResourceVault>();
            TextChange(_resourceType, resourceVault.GetResourceAmount(_resourceType));

            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void TextChange(ResourceType resourceType, int amount)
        {
            if (_resourceText == null || _resourceType != resourceType)
                return;
            
            _resourceText.text = amount.ToString();
        }
        
        private void Subscribe()
        {
            resourceVault.OnResourceChanged += TextChange;
        }

        private void Unsubscribe()
        {
            resourceVault.OnResourceChanged -= TextChange;
        }
    }
}