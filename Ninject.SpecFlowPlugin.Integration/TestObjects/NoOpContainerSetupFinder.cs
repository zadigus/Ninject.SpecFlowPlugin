namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration.TestObjects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::Ninject;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.ContainerLookup;

    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "instantiation is indirect")]
    internal class NoOpContainerSetupFinder<T> : ContainerSetupFinder<T, IKernel>
        where T : ContainerConfigurationAttribute
    {
        public NoOpContainerSetupFinder()
            : base(null)
        {
        }

        protected override Action<IKernel> FindSetupContainer()
        {
            return kernel => { };
        }
    }
}