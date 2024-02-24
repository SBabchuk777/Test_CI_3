using Tools.MaxCore.Scripts.Project.DI.ProjectInjector;
using UnityEngine;

namespace Tools.MaxCore.Scripts.Project.DI
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private ProjectInstaller installer;
        
        public Injector Injector { get; private set; }
        public static ProjectContext Instance { get; private set; }
        
        private void Awake()
        {
            RunInjector();
            CreateSingleton();
        }

        private void RunInjector()
        {
            Injector = new Injector(installer);
            Injector.Inject();
        }


        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                NotifyIInitializable();
                DontDestroyOnLoad(gameObject);
            }
        }

        private void NotifyIInitializable()
        {
            var initializableComponents = GetComponentsInChildren<IProjectInitializable>(true);

            foreach (var initializable in initializableComponents)
                initializable.Initialize();
        }

        public TDependency GetDependence<TDependency>() => 
            Injector.Container.Resolve<TDependency>();
    }
}