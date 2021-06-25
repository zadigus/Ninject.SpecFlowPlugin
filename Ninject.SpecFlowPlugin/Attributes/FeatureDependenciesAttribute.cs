namespace Ninject.SpecFlowPlugin.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FeatureDependenciesAttribute : ContainerConfigurationAttribute
    {
    }
}