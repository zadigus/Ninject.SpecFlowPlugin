namespace SpecFlowPlugin.Acceptance.Common.StepDefinitions
{
    using FluentAssertions;
    using SpecFlowPlugin.Acceptance.Common.TestClasses;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepClassDisposableAfterFeature
    {
        private readonly ISingletonDisposableFeatureDependency singletonDisposableInstance;

        private readonly TestThreadContext testThreadContext;

        private readonly ITransientDisposableFeatureDependency transientDisposableFeatureDependency;

        public StepClassDisposableAfterFeature(
            ISingletonDisposableFeatureDependency singletonDisposableInstance,
            ITransientDisposableFeatureDependency transientDisposableFeatureDependency,
            TestThreadContext testThreadContext)
        {
            this.singletonDisposableInstance = singletonDisposableInstance;
            this.transientDisposableFeatureDependency = transientDisposableFeatureDependency;
            this.testThreadContext = testThreadContext;
        }

        [Given(
            @"I have injected a singleton disposable feature dependency in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedASingletonDisposableFeatureDependencyInTheBindingClassStepClassDisposableAfterFeature()
        {
            // here we cannot proceed the same way as for scenario dependencies, because the
            // AfterTestRun does not support tag filtering, therefore this little hack
            this.testThreadContext.Set(true, ContextKeys.MustDisposeFeatureDependency.ToString());
            this.singletonDisposableInstance.Should().BeOfType<SingletonDisposableFeatureDependency>();
        }

        [Given(
            @"I have injected a transient disposable feature dependency in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedATransientDisposableFeatureDependencyInTheBindingClassStepClassDisposableAfterFeature()
        {
            // here we cannot proceed the same way as for scenario dependencies, because the
            // AfterTestRun does not support tag filtering, therefore this little hack
            this.testThreadContext.Set(true, ContextKeys.MustDisposeFeatureDependency.ToString());
            this.transientDisposableFeatureDependency.Should().BeOfType<TransientDisposableFeatureDependency>();
        }
    }
}