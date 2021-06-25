namespace WrongAfterScenarioHookOrder
{
    using System.Diagnostics.CodeAnalysis;
    using Ninject;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.Extensions;

    internal class DependenciesConfigurator
    {
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [ScenarioDependencies]
        public static void SetupScenarioContainer(IKernel kernel)
        {
            kernel.RegisterBindings<DependenciesConfigurator>();
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [FeatureDependencies]
        public static void SetupFeatureContainer(IKernel kernel)
        {
            kernel.RegisterBindings<DependenciesConfigurator>();
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [TestThreadDependencies]
        public static void SetupTestThreadContainer(IKernel kernel)
        {
            kernel.RegisterBindings<DependenciesConfigurator>();
        }
    }
}