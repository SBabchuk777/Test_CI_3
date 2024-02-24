using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using Tools.MaxCore.Scripts.Services.UIViewService.Data;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService
{
    public class UIViewFactory
    {
        private UIViewData ViewData { get; }

        public UIViewFactory(UIViewData viewData) => 
            ViewData = viewData;

        private Injector Injector => 
            ProjectContext.Instance.Injector;

        public BaseView InstantiatePrefab(UIViewType type, Transform transform)
        {
            var instance = Object.Instantiate(ViewData.ViewDataMap[type].gameObject, Vector3.zero, Quaternion.identity, transform)
                .GetComponent<BaseView>();
            
            instance.transform.localPosition = Vector3.zero;
            foreach (var component in instance.GetComponentsInChildren<MonoBehaviour>(true))
            {
                Injector.InjectDependenciesInObject(component);
            }
            
            return instance;
        }
    }
}