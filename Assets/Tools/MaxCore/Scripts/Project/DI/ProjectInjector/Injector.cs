using System.Reflection;

namespace Tools.MaxCore.Scripts.Project.DI.ProjectInjector
{
    public class Injector
    {
        public DIContainer Container { get; set; }
        
        public Injector(ProjectInstaller installer) => 
            Container = installer.RegisterDependencies();
        
        public void Inject()
        {
            var allDependency = Container.AllDependency();
            
            foreach (var dependency in allDependency) 
                InjectDependenciesInObject(dependency);
        }
        public void InjectDependenciesInObject(object target)
        {
            InjectFields(target);
            InjectMethods(target);
        }

        private void InjectMethods(object target)
        {
            var targetType = target.GetType();
            var methods = targetType.GetMethods();

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0)
                {
                    var parameters = method.GetParameters();
                    var arguments = new object[parameters.Length];

                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var parameterType = parameters[i].ParameterType;
                        var resolvedDependency = Container.Resolve(parameterType);
                        arguments[i] = resolvedDependency;
                    }

                    method.Invoke(target, arguments);
                }
            }
        }

        private void InjectFields(object target)
        {
            var targetType = target.GetType();
            var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field.GetCustomAttributes(typeof(InjectAttribute), false).Length > 0)
                {
                    var fieldType = field.FieldType;
                    var resolvedDependency = Container.Resolve(fieldType);
                    field.SetValue(target, resolvedDependency);
                }
            }
        }
        
       
    }
}