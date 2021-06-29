#if DEBUG || RELEASE
namespace SpecFlowPlugin.Acceptance
{
    using System;
    using Ninject;
    using SpecFlowPlugin.Acceptance.TestClasses;
    using SpecFlowPluginBase;
    using SpecFlowPluginBase.Attributes;

    internal class DependenciesConfigurator
    {
        private static readonly Action<Type, IKernel> BindTypeInSingletonScope =
            (type, kernel) => kernel.Bind(type).ToSelf().InSingletonScope();

        [ScenarioDependencies]
        public static void SetupScenarioContainer(IKernel kernel)
        {
            // register bindings from Acceptance project
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));

            // we only test the disposal of objects in singleton scope because
            // that's the only case handled by BoDi; singletons are disposed,
            // transient aren't
            kernel.Bind<ISingletonDisposableScenarioDependency1>()
                .To<SingletonDisposableScenarioDependency1>()
                .InSingletonScope();
            kernel.Bind<ISingletonDisposableScenarioDependency2>()
                .To<SingletonDisposableScenarioDependency2>()
                .InSingletonScope();
            kernel.Bind<ITransientScenarioDependency>().To<TransientScenarioDependency>();
            kernel.Bind<ISingletonScenarioDependency>().To<SingletonScenarioDependency>().InSingletonScope();
            kernel.Bind<ITransientDisposableScenarioDependency1>().To<TransientDisposableScenarioDependency1>();
            kernel.Bind<ITransientDisposableScenarioDependency2>().To<TransientDisposableScenarioDependency2>();
        }

        [FeatureDependencies]
        public static void SetupFeatureContainer(IKernel kernel)
        {
            // register bindings from Acceptance project
            BindingRegister.RegisterBindings<DependenciesConfigurator>(type => BindTypeInSingletonScope(type, kernel));

            // we only test the disposal of objects in singleton scope because
            // that's the only case handled by BoDi; singletons are disposed,
            // transient aren't
            kernel.Bind<ISingletonDisposableFeatureDependency1>()
                .To<SingletonDisposableFeatureDependency1>()
                .InSingletonScope();
            kernel.Bind<ISingletonDisposableFeatureDependency2>()
                .To<SingletonDisposableFeatureDependency2>()
                .InSingletonScope();
            kernel.Bind<ITransientFeatureDependency>().To<TransientFeatureDependency>();
            kernel.Bind<ISingletonFeatureDependency>().To<SingletonFeatureDependency>().InSingletonScope();
            kernel.Bind<ITransientDisposableFeatureDependency1>().To<TransientDisposableFeatureDependency1>();
            kernel.Bind<ITransientDisposableFeatureDependency2>().To<TransientDisposableFeatureDependency2>();
        }
    }
}
#endif