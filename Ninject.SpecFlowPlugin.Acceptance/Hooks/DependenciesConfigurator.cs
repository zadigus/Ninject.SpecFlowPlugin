#if DEBUGBODI || RELEASEBODI
namespace Ninject.SpecFlowPlugin.Acceptance.Hooks
{
    using BoDi;
    using Ninject.SpecFlowPlugin.Acceptance.TestClasses;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class DependenciesConfigurator
    {
        private readonly IObjectContainer objectContainer;

        public DependenciesConfigurator(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeFeature]
        public static void SetupFeatureContainer(IObjectContainer objectContainer)
        {
            // types are registered as InstancePerContext by default
            // DisposableFeatureDependency is disposed because it is disposable AND a singleton within the DI context
            // truly transient disposable objects are not disposed by SpecFlow after each feature
            objectContainer
                .RegisterTypeAs<SingletonDisposableFeatureDependency1, ISingletonDisposableFeatureDependency1>();
            objectContainer
                .RegisterTypeAs<SingletonDisposableFeatureDependency2, ISingletonDisposableFeatureDependency2>();
            objectContainer.RegisterTypeAs<TransientFeatureDependency, ITransientFeatureDependency>()
                .InstancePerDependency();
            objectContainer.RegisterTypeAs<SingletonFeatureDependency, ISingletonFeatureDependency>();

            // only here to be able to get the StepClassDisposableAfterFeature class
            objectContainer
                .RegisterTypeAs<TransientDisposableFeatureDependency1, ITransientDisposableFeatureDependency1>()
                .InstancePerDependency();
            objectContainer
                .RegisterTypeAs<TransientDisposableFeatureDependency2, ITransientDisposableFeatureDependency2>()
                .InstancePerDependency();
        }

        [BeforeScenario]
        public void SetupScenarioContainer()
        {
            // types are registered as InstancePerContext by default
            // DisposableScenarioDependency is disposed because it is disposable AND a singleton within the DI context
            // truly transient disposable objects are not disposed by SpecFlow after each scenario
            this.objectContainer
                .RegisterTypeAs<SingletonDisposableScenarioDependency1, ISingletonDisposableScenarioDependency1>();
            this.objectContainer
                .RegisterTypeAs<SingletonDisposableScenarioDependency2, ISingletonDisposableScenarioDependency2>();
            this.objectContainer.RegisterTypeAs<TransientScenarioDependency, ITransientScenarioDependency>()
                .InstancePerDependency();
            this.objectContainer.RegisterTypeAs<SingletonScenarioDependency, ISingletonScenarioDependency>();

            // only here to be able to get the StepClassDisposableAfterScenario class
            this.objectContainer
                .RegisterTypeAs<TransientDisposableScenarioDependency1, ITransientDisposableScenarioDependency1>()
                .InstancePerDependency();
            this.objectContainer
                .RegisterTypeAs<TransientDisposableScenarioDependency2, ITransientDisposableScenarioDependency2>()
                .InstancePerDependency();
        }
    }
}
#endif