using Tools.MaxCore.Scripts.Project.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.MaxCore.Scripts.Services.UIViewService
{
    [RequireComponent(typeof(Button))]
    public class UIViewCreatorButton : MonoBehaviour
    {
        public UIViewType UIViewType;
        
        private UIViewService UIViewService => ProjectContext.Instance.GetDependence<UIViewService>();

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Open);
        }

        private void Open()
        {
            UIViewService.InstantiateAsync(UIViewType);
        }
    }
}