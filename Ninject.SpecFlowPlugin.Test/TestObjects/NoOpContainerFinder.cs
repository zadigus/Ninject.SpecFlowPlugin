namespace Ninject.SpecFlowPlugin.Test.TestObjects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.ContainerLookup;

    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "instantiation is indirect")]
    internal class NoOpContainerFinder<T> : ContainerFinder<T>
        where T : ContainerConfigurationAttribute
    {
        public NoOpContainerFinder()
            : base(null)
        {
        }

        protected override Action<IKernel> FindSetupContainer()
        {
            return kernel => { };
        }
    }
}