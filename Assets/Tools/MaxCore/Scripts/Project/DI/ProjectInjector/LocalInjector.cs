using UnityEngine;

namespace Tools.MaxCore.Scripts.Project.DI.ProjectInjector
{
    public class LocalInjector : MonoBehaviour
    {
        private Injector Injector => ProjectContext.Instance.Injector;
        
        private void Awake()
        {
            GetInitializeObjects();
        }

        private void GetInitializeObjects()
        {
            foreach (var behaviour in GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (behaviour == this) 
                    continue;
                
                Injector.InjectDependenciesInObject(behaviour);
            }
        }
    }
}