namespace SpecFlowPluginBase.Attributes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage(
        "Microsoft.Performance",
        "CA1813:AvoidUnsealedAttributes",
        Justification = "need it so for the sake of clarity")]
    [AttributeUsage(AttributeTargets.Method)]
    public class ContainerConfigurationAttribute : Attribute
    {
    }
}