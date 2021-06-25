namespace WrongAfterScenarioHookOrder
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Ninject;
    using SpecFlowPluginBase;
    using SpecFlowPluginBase.Attributes;

    internal class DependenciesConfigurator
    {
        private static readonly Action<Type, IKernel> BindTypeInSingletonScope =
            (type, kernel) => kernel.Bind(type).ToSelf().InSingletonScope();

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [ScenarioDependencies]
        public static void SetupScenarioContainer(IKernel kernel)
        {
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [FeatureDependencies]
        public static void SetupFeatureContainer(IKernel kernel)
        {
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [TestThreadDependencies]
        public static void SetupTestThreadContainer(IKernel kernel)
        {
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));
        }
    }
}