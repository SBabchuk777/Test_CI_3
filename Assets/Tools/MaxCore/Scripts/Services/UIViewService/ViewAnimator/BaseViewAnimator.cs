using System;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Services.UIViewService.ViewAnimator
{
    public abstract class BaseViewAnimator : MonoBehaviour
    {
        protected BaseView BaseView => GetComponent<BaseView>();
        public abstract void Open(Action callback);
        public abstract void Close();
       
    }
}