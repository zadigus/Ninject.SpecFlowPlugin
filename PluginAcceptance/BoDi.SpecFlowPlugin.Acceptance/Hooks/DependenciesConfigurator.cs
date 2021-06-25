namespace BoDi.SpecFlowPlugin.Acceptance.Hooks
{
    using global::SpecFlowPlugin.Acceptance.Common.TestClasses;
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
                .RegisterTypeAs<SingletonDisposableFeatureDependency, ISingletonDisposableFeatureDependency>();
            objectContainer.RegisterTypeAs<TransientFeatureDependency, ITransientFeatureDependency>()
                .InstancePerDependency();
            objectContainer.RegisterTypeAs<SingletonFeatureDependency, ISingletonFeatureDependency>();

            // only here to be able to get the StepClassDisposableAfterFeature class
            objectContainer
                .RegisterTypeAs<TransientDisposableFeatureDependency, ITransientDisposableFeatureDependency>();
        }

        [BeforeScenario]
        public void SetupScenarioContainer()
        {
            // types are registered as InstancePerContext by default
            // DisposableScenarioDependency is disposed because it is disposable AND a singleton within the DI context
            // truly transient disposable objects are not disposed by SpecFlow after each scenario
            this.objectContainer
                .RegisterTypeAs<SingletonDisposableScenarioDependency, ISingletonDisposableScenarioDependency>();
            this.objectContainer.RegisterTypeAs<TransientScenarioDependency, ITransientScenarioDependency>()
                .InstancePerDependency();
            this.objectContainer.RegisterTypeAs<SingletonScenarioDependency, ISingletonScenarioDependency>();

            // only here to be able to get the StepClassDisposableAfterScenario class
            this.objectContainer
                .RegisterTypeAs<TransientDisposableScenarioDependency, ITransientDisposableScenarioDependency>();
        }
    }
}