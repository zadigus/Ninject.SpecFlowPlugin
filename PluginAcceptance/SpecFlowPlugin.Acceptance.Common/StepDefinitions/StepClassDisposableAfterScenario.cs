namespace SpecFlowPlugin.Acceptance.Common.StepDefinitions
{
    using FluentAssertions;
    using SpecFlowPlugin.Acceptance.Common.TestClasses;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepClassDisposableAfterScenario
    {
        private readonly ISingletonDisposableScenarioDependency singletonDisposableInstance;

        private readonly ITransientDisposableScenarioDependency transientDisposableScenarioDependency;

        public StepClassDisposableAfterScenario(
            ISingletonDisposableScenarioDependency singletonDisposableInstance,
            ITransientDisposableScenarioDependency transientDisposableScenarioDependency)
        {
            this.singletonDisposableInstance = singletonDisposableInstance;
            this.transientDisposableScenarioDependency = transientDisposableScenarioDependency;
        }

        [Given(
            @"I have injected a singleton disposable scenario dependency in the binding class StepClassDisposableAfterScenario")]
        public void
            GivenIHaveInjectedASingletonDisposableScenarioDependencyInTheBindingClassStepClassDisposableAfterScenario()
        {
            this.singletonDisposableInstance.Should().BeOfType<SingletonDisposableScenarioDependency>();
        }

        [Given(
            @"I have injected a transient disposable scenario dependency in the binding class StepClassDisposableAfterScenario")]
        public void
            GivenIHaveInjectedATransientDisposableScenarioDependencyInTheBindingClassStepClassDisposableAfterScenario()
        {
            this.transientDisposableScenarioDependency.Should().BeOfType<TransientDisposableScenarioDependency>();
        }
    }
}