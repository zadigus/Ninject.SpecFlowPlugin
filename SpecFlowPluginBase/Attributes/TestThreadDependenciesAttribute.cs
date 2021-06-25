namespace SpecFlowPluginBase.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestThreadDependenciesAttribute : ContainerConfigurationAttribute
    {
    }
}