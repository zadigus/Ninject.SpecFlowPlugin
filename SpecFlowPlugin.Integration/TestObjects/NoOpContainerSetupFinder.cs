namespace SpecFlowPlugin.Integration.TestObjects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.ContainerLookup;

    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "instantiation is indirect")]
    internal class NoOpContainerSetupFinder<T, TContainerType> : ContainerSetupFinder<T, TContainerType>
        where T : ContainerConfigurationAttribute
        where TContainerType : class
    {
        public NoOpContainerSetupFinder()
            : base(null)
        {
        }

        protected override Action<TContainerType> FindSetupContainer()
        {
            return kernel => { };
        }
    }
}