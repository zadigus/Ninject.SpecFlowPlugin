namespace Ninject.SpecFlowPlugin.Acceptance
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::SpecFlowPlugin.Acceptance.Common.TestClasses;
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
            // register bindings from Acceptance project
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));

            // register bindings from Acceptance.Common project
            BindingRegister.RegisterBindings<TransientScenarioDependency>(
                type => BindTypeInSingletonScope(type, kernel));

            // we only test the disposal of objects in singleton scope because
            // that's the only case handled by BoDi; singletons are disposed,
            // transient aren't
            kernel.Bind<ISingletonDisposableScenarioDependency>()
                .To<SingletonDisposableScenarioDependency>()
                .InSingletonScope();
            kernel.Bind<ITransientScenarioDependency>().To<TransientScenarioDependency>();
            kernel.Bind<ISingletonScenarioDependency>().To<SingletonScenarioDependency>().InSingletonScope();
            kernel.Bind<ITransientDisposableScenarioDependency>().To<TransientDisposableScenarioDependency>();
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [FeatureDependencies]
        public static void SetupFeatureContainer(IKernel kernel)
        {
            // register bindings from Acceptance project
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));

            // register bindings from Acceptance.Common project
            BindingRegister.RegisterBindings<TransientScenarioDependency>(
                type => BindTypeInSingletonScope(type, kernel));

            // we only test the disposal of objects in singleton scope because
            // that's the only case handled by BoDi; singletons are disposed,
            // transient aren't
            kernel.Bind<ISingletonDisposableFeatureDependency>()
                .To<SingletonDisposableFeatureDependency>()
                .InSingletonScope();
            kernel.Bind<ITransientFeatureDependency>().To<TransientFeatureDependency>();
            kernel.Bind<ISingletonFeatureDependency>().To<SingletonFeatureDependency>().InSingletonScope();
            kernel.Bind<ITransientDisposableFeatureDependency>().To<TransientDisposableFeatureDependency>();
        }
    }
}