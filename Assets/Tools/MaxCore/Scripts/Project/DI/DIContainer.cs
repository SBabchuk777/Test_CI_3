using System;
using System.Collections.Generic;

namespace Tools.MaxCore.Scripts.Project.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, object> dependencies = new Dictionary<Type, object>();

        public void Register<TDependency>(TDependency dependency)
        {
            dependencies[typeof(TDependency)] = dependency;
        }

        public TDependency Resolve<TDependency>()
        {
            if (dependencies.TryGetValue(typeof(TDependency), out object dependency))
            {
                return (TDependency)dependency;
            }

            throw new Exception($"Dependency of type {typeof(TDependency)} is not registered.");
        }
        
        public object Resolve(Type dependencyType)
        {
            if (dependencies.TryGetValue(dependencyType, out object dependency))
            {
                return dependency;
            }

            throw new Exception($"Dependency of type {dependencyType} is not registered.");
        }

        public HashSet<object> AllDependency()
        {
            var dependenceHash = new HashSet<object>();

            foreach (var dependenciesValue in dependencies.Values) 
                dependenceHash.Add(dependenciesValue);
            
            return dependenceHash;
        }
    }
}