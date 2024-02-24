using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using JetBrains.Annotations;
using Tools.MaxCore.Scripts.Project.DI;
using Tools.MaxCore.Scripts.Services.UIViewService.Data;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService
{
    public class UIViewService : MonoBehaviour, IProjectInitializable
    {
        [SerializeField] private UIViewData ViewData;
        
        [SerializeField] private Transform _layer_1;
        [SerializeField] private Transform _layer_2;

        private UIViewFactory factory;

        private List<BaseView> Views { get; set; }

        public void Initialize()
        {
            factory = new UIViewFactory(ViewData);
            Views = new List<BaseView>();
        }

        public BaseView Instantiate(UIViewType type, int priority = 0)
        {
            var parent = priority == 0 ? _layer_1 : _layer_2;
            var instance = factory.InstantiatePrefab(type, parent);
            instance.SetView(type);
            instance.OnCloseView += () => RemoveView(instance);
            
            Views.Add(instance);

            return instance;
        }

        public void InstantiateAsync(UIViewType type , float delay = 0.1f)
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                Instantiate(type);
            }).Play();
        }

        public void RemoveAllViews()
        {
            foreach (var baseView in Views)
            {
                baseView.DestroyView();
            }
        }

        public void DestroyView(BaseView view)
        {
            if (view == null)
            {
                return;
            }
            view.DestroyView();
            RemoveView(view);
        }
        
        public void RemoveView(BaseView view)
        {
            if (view == null)
            {
                return;
            }
            
           
            Views.Remove(view);
        }

        public BaseView GetView(UIViewType view)
        {
           var baseView = Views.FirstOrDefault(v => v.UIViewType == view);
           return baseView != null ? baseView : null;
        }
    }
}