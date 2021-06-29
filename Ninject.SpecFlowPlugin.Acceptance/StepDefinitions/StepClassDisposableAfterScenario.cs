namespace Ninject.SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using Ninject.SpecFlowPlugin.Acceptance.TestClasses;
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public class StepClassDisposableAfterScenario
    {
        private readonly FeatureContext featureContext;

        private readonly ISingletonDisposableScenarioDependency1 singletonDisposableScenarioDependency1;

        private readonly ISingletonDisposableScenarioDependency2 singletonDisposableScenarioDependency2;

        private readonly ITransientDisposableScenarioDependency1 transientDisposableScenarioDependency1;

        private readonly ITransientDisposableScenarioDependency2 transientDisposableScenarioDependency2;

        public StepClassDisposableAfterScenario(
            FeatureContext featureContext,
            ISingletonDisposableScenarioDependency1 singletonDisposableScenarioDependency1,
            ISingletonDisposableScenarioDependency2 singletonDisposableScenarioDependency2,
            ITransientDisposableScenarioDependency1 transientDisposableScenarioDependency1,
            ITransientDisposableScenarioDependency2 transientDisposableScenarioDependency2)
        {
            this.featureContext = featureContext;
            this.singletonDisposableScenarioDependency1 = singletonDisposableScenarioDependency1;
            this.singletonDisposableScenarioDependency2 = singletonDisposableScenarioDependency2;
            this.transientDisposableScenarioDependency1 = transientDisposableScenarioDependency1;
            this.transientDisposableScenarioDependency2 = transientDisposableScenarioDependency2;
        }

        [Given(
            @"I have injected SingletonDisposableScenarioDependency(.*) in the binding class StepClassDisposableAfterScenario")]
        public void
            GivenIHaveInjectedSingletonDisposableScenarioDependencyInTheBindingClassStepClassDisposableAfterScenario(
                int injectedDependencyNb)
        {
            if (injectedDependencyNb == 1)
            {
                this.featureContext.Save(true, ContextKeys.MustDisposeSingletonScenarioDependency1);
                this.singletonDisposableScenarioDependency1.Should().BeOfType<SingletonDisposableScenarioDependency1>();
            }
            else
            {
                this.featureContext.Save(true, ContextKeys.MustDisposeSingletonScenarioDependency2);
                this.singletonDisposableScenarioDependency2.Should().BeOfType<SingletonDisposableScenarioDependency2>();
            }
        }

        [Given(
            @"I have injected TransientDisposableScenarioDependency(.*) in the binding class StepClassDisposableAfterScenario")]
        public void
            GivenIHaveInjectedATransientDisposableScenarioDependencyInTheBindingClassStepClassDisposableAfterScenario(
                int injectedDependencyNb)
        {
            if (injectedDependencyNb == 1)
            {
                this.featureContext.Save(true, ContextKeys.MustDisposeTransientScenarioDependency1);
                this.transientDisposableScenarioDependency1.Should().BeOfType<TransientDisposableScenarioDependency1>();
            }
            else
            {
                this.featureContext.Save(true, ContextKeys.MustDisposeTransientScenarioDependency2);
                this.transientDisposableScenarioDependency2.Should().BeOfType<TransientDisposableScenarioDependency2>();
            }
        }

        [Then(
            @"SingletonDisposableScenarioDependency(.*) has been disposed if the previous scenario had to dispose it")]
        public void ThenSingletonDisposableScenarioDependencyHasBeenDisposedIfThePreviousScenarioHadToDisposeIt(
            int injectedDependencyNb)
        {
            if (injectedDependencyNb == 1)
            {
                var mustHaveDisposedDependency1 =
                    this.featureContext.Get<bool>(ContextKeys.MustDisposeSingletonScenarioDependency1);
                var dependency1Disposed =
                    this.featureContext.Get<bool>(ContextKeys.SingletonDisposableScenarioDependency1IsDisposed);
                dependency1Disposed.Should().Be(mustHaveDisposedDependency1);
            }
            else
            {
                var mustHaveDisposedDependency2 =
                    this.featureContext.Get<bool>(ContextKeys.MustDisposeSingletonScenarioDependency2);
                var dependency2Disposed =
                    this.featureContext.Get<bool>(ContextKeys.SingletonDisposableScenarioDependency2IsDisposed);
                dependency2Disposed.Should().Be(mustHaveDisposedDependency2);
            }
        }

        [Then(
            @"TransientDisposableScenarioDependency(.*) has been disposed if the previous scenario had to dispose it")]
        public void ThenTransientDisposableScenarioDependencyHasBeenDisposedIfThePreviousScenarioHadToDisposeIt(
            int injectedDependencyNb)
        {
#if DEBUG || RELEASE
            if (injectedDependencyNb == 1)
            {
                var mustHaveDisposedDependency1 =
                    this.featureContext.Get<bool>(ContextKeys.MustDisposeTransientScenarioDependency1);
                var dependency1Disposed =
                    this.featureContext.Get<bool>(ContextKeys.TransientDisposableScenarioDependency1IsDisposed);
                dependency1Disposed.Should().Be(mustHaveDisposedDependency1);
            }
            else
            {
                var mustHaveDisposedDependency2 =
                    this.featureContext.Get<bool>(ContextKeys.MustDisposeTransientScenarioDependency2);
                var dependency2Disposed =
                    this.featureContext.Get<bool>(ContextKeys.TransientDisposableScenarioDependency2IsDisposed);
                dependency2Disposed.Should().Be(mustHaveDisposedDependency2);
            }
#endif
        }
    }
}