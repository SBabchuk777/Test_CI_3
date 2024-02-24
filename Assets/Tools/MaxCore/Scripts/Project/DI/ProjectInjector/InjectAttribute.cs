using System;

namespace Tools.MaxCore.Scripts.Project.DI.ProjectInjector
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
    }
}