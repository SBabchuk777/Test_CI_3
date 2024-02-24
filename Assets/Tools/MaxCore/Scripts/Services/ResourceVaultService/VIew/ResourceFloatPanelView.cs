using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.Services.ResourceVaultService
{
    public class ResourceFloatPanelView : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Text _resourceText;

        private ResourceVault resourceVault;
        
        private void Start()
        {
            resourceVault = ProjectContext.Instance.GetDependence<ResourceVault>();
            TextChange(_resourceType, resourceVault.GetFloatResourceAmount(_resourceType));

            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void TextChange(ResourceType resourceType, float amount)
        {
            if (_resourceText == null || _resourceType != resourceType)
                return;
            
            _resourceText.text = amount.ToString("F2");
        }
        
        private void Subscribe()
        {
            resourceVault.OnFloatResourceChanged += TextChange;
        }

        private void Unsubscribe()
        {
            resourceVault.OnFloatResourceChanged -= TextChange;
        }
    }
}