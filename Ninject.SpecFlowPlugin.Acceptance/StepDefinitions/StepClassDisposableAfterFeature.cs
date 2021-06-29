namespace Ninject.SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using Ninject.SpecFlowPlugin.Acceptance.TestClasses;
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepClassDisposableAfterFeature
    {
        private readonly ISingletonDisposableFeatureDependency1 singletonDisposableInstance1;

        private readonly ISingletonDisposableFeatureDependency2 singletonDisposableInstance2;

        private readonly TestThreadContext testThreadContext;

        private readonly ITransientDisposableFeatureDependency1 transientDisposableFeatureDependency1;

        private readonly ITransientDisposableFeatureDependency2 transientDisposableFeatureDependency2;

        public StepClassDisposableAfterFeature(
            ISingletonDisposableFeatureDependency1 singletonDisposableInstance1,
            ISingletonDisposableFeatureDependency2 singletonDisposableInstance2,
            ITransientDisposableFeatureDependency1 transientDisposableFeatureDependency1,
            ITransientDisposableFeatureDependency2 transientDisposableFeatureDependency2,
            TestThreadContext testThreadContext)
        {
            this.singletonDisposableInstance1 = singletonDisposableInstance1;
            this.singletonDisposableInstance2 = singletonDisposableInstance2;
            this.transientDisposableFeatureDependency1 = transientDisposableFeatureDependency1;
            this.transientDisposableFeatureDependency2 = transientDisposableFeatureDependency2;
            this.testThreadContext = testThreadContext;
        }

        [Given(
            @"I have injected SingletonDisposableFeatureDependency1 in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedSingletonDisposableFeatureDependency1InTheBindingClassStepClassDisposableAfterFeature()
        {
            this.testThreadContext.Save(true, ContextKeys.MustDisposeSingletonFeatureDependency1);
            this.singletonDisposableInstance1.Should().BeOfType<SingletonDisposableFeatureDependency1>();
        }

        [Given(
            @"I have injected SingletonDisposableFeatureDependency2 in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedSingletonDisposableFeatureDependency2InTheBindingClassStepClassDisposableAfterFeature()
        {
            this.testThreadContext.Save(true, ContextKeys.MustDisposeSingletonFeatureDependency2);
            this.singletonDisposableInstance2.Should().BeOfType<SingletonDisposableFeatureDependency2>();
        }

        [Given(
            @"I have injected TransientDisposableFeatureDependency1 in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedATransientDisposableFeatureDependency1InTheBindingClassStepClassDisposableAfterFeature()
        {
            this.testThreadContext.Save(true, ContextKeys.MustDisposeTransientFeatureDependency1);
            this.transientDisposableFeatureDependency1.Should().BeOfType<TransientDisposableFeatureDependency1>();
        }

        [Given(
            @"I have injected TransientDisposableFeatureDependency2 in the binding class StepClassDisposableAfterFeature")]
        public void
            GivenIHaveInjectedATransientDisposableFeatureDependency2InTheBindingClassStepClassDisposableAfterFeature()
        {
            this.testThreadContext.Save(true, ContextKeys.MustDisposeTransientFeatureDependency2);
            this.transientDisposableFeatureDependency2.Should().BeOfType<TransientDisposableFeatureDependency2>();
        }

        [Then(@"SingletonDisposableFeatureDependency2 has been disposed if the previous feature had to dispose it")]
        public void ThenSingletonDisposableFeatureDependency2HasBeenDisposedIfThePreviousFeatureHadToDisposeIt()
        {
            var mustHaveDisposedDependency2 =
                this.testThreadContext.Get<bool>(ContextKeys.MustDisposeSingletonFeatureDependency2);
            var dependency2Disposed =
                this.testThreadContext.Get<bool>(ContextKeys.SingletonDisposableFeatureDependency2IsDisposed);
            dependency2Disposed.Should().Be(mustHaveDisposedDependency2);
        }

        [Then(@"SingletonDisposableFeatureDependency1 has been disposed if the previous feature had to dispose it")]
        public void ThenSingletonDisposableFeatureDependency1HasBeenDisposedIfThePreviousFeatureHadToDisposeIt()
        {
            var mustHaveDisposedDependency1 =
                this.testThreadContext.Get<bool>(ContextKeys.MustDisposeSingletonFeatureDependency1);
            var dependency1Disposed =
                this.testThreadContext.Get<bool>(ContextKeys.SingletonDisposableFeatureDependency1IsDisposed);
            dependency1Disposed.Should().Be(mustHaveDisposedDependency1);
        }

        [Then(@"TransientDisposableFeatureDependency1 has been disposed if the previous feature had to dispose it")]
        public void ThenTransientDisposableFeatureDependency1HasBeenDisposedIfThePreviousFeatureHadToDisposeIt()
        {
#if DEBUG || RELEASE
            var mustHaveDisposedDependency1 =
                this.testThreadContext.Get<bool>(ContextKeys.MustDisposeTransientFeatureDependency1);
            var dependency1Disposed =
                this.testThreadContext.Get<bool>(ContextKeys.TransientDisposableFeatureDependency1IsDisposed);
            dependency1Disposed.Should().Be(mustHaveDisposedDependency1);
#endif
        }

        [Then(@"TransientDisposableFeatureDependency2 has been disposed if the previous feature had to dispose it")]
        public void ThenTransientDisposableFeatureDependency2HasBeenDisposedIfThePreviousFeatureHadToDisposeIt()
        {
#if DEBUG || RELEASE
            var mustHaveDisposedDependency2 =
                this.testThreadContext.Get<bool>(ContextKeys.MustDisposeTransientFeatureDependency2);
            var dependency2Disposed =
                this.testThreadContext.Get<bool>(ContextKeys.TransientDisposableFeatureDependency2IsDisposed);
            dependency2Disposed.Should().Be(mustHaveDisposedDependency2);
#endif
        }
    }
}