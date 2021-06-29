namespace SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using SpecFlowPlugin.Acceptance.TestClasses;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class StepClass2
    {
        private readonly FeatureContext featureContext;

        private readonly ScenarioContext scenarioContext;

        private readonly ISingletonFeatureDependency singletonFeatureDependency;

        private readonly ISingletonScenarioDependency singletonScenarioDependency;

        private readonly ITransientFeatureDependency transientFeatureDependency;

        private readonly ITransientScenarioDependency transientScenarioDependency;

        public StepClass2(
            FeatureContext featureContext,
            ScenarioContext scenarioContext,
            ISingletonFeatureDependency singletonFeatureDependency,
            ITransientFeatureDependency transientFeatureDependency,
            ISingletonScenarioDependency singletonScenarioDependency,
            ITransientScenarioDependency transientScenarioDependency)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
            this.singletonFeatureDependency = singletonFeatureDependency;
            this.transientFeatureDependency = transientFeatureDependency;
            this.singletonScenarioDependency = singletonScenarioDependency;
            this.transientScenarioDependency = transientScenarioDependency;
        }

        [Given(@"I have injected TransientScenarioDependency in the binding class StepClass2")]
        public void GivenIInjectTransientScenarioDependencyInTheBindingClassStepClass()
        {
            this.transientScenarioDependency.Should().BeOfType<TransientScenarioDependency>();
        }

        [Given(@"I have injected TransientFeatureDependency in the binding class StepClass2")]
        public void GivenIInjectTransientFeatureDependencyInTheBindingClassStepClass()
        {
            this.transientFeatureDependency.Should().BeOfType<TransientFeatureDependency>();
        }

        [Given(@"I have injected SingletonScenarioDependency in the binding class StepClass2")]
        public void GivenIInjectSingletonScenarioDependencyInTheBindingClassStepClass()
        {
            this.singletonScenarioDependency.Should().BeOfType<SingletonScenarioDependency>();
        }

        [Given(@"I have injected SingletonFeatureDependency in the binding class StepClass2")]
        public void GivenIInjectSingletonFeatureDependencyInTheBindingClassStepClass()
        {
            this.singletonFeatureDependency.Should().BeOfType<SingletonFeatureDependency>();
        }

        [Given(@"I have injected the scenario context in the binding class StepClass2")]
        public void GivenIHaveInjectedTheScenarioContextInTheBindingClassStepClass()
        {
            this.scenarioContext.Should().BeOfType<ScenarioContext>();
        }

        [Given(@"I have injected the feature context in the binding class StepClass2")]
        public void GivenIHaveInjectedTheFeatureContextInTheBindingClassStepClass()
        {
            this.featureContext.Should().BeOfType<FeatureContext>();
        }

        [When(@"I set property MyProp of TransientScenarioDependency to '(.*)' in StepClass2")]
        public void WhenISetPropertyMyPropOfTransientScenarioDependencyToInStepClass(string value)
        {
            this.transientScenarioDependency.MyProp = value;
        }

        [When(@"I set property MyProp of TransientFeatureDependency to '(.*)' in StepClass2")]
        public void WhenISetPropertyMyPropOfTransientFeatureDependencyToInStepClass(string value)
        {
            this.transientFeatureDependency.MyProp = value;
        }

        [When(@"I remove the key '(.*)' from the feature context in StepClass2")]
        public void WhenIRemoveTheKeyFromTheFeatureContextInStepClass(string key)
        {
            this.featureContext.Remove(key);
        }

        [Then(@"the property MyProp of SingletonScenarioDependency has value '(.*)' in StepClass2")]
        public void ThenThePropertyMyPropOfSingletonScenarioDependencyHasValueInStepClass(string value)
        {
            this.singletonScenarioDependency.MyProp.Should().Be(value);
        }

        [Then(@"the property MyProp of SingletonFeatureDependency has value '(.*)' in StepClass2")]
        public void ThenThePropertyMyPropOfSingletonFeatureDependencyHasValueInStepClass(string value)
        {
            this.singletonFeatureDependency.MyProp.Should().Be(value);
        }

        [Then(@"the scenario context delivers the value '(.*)' for key '(.*)' in StepClass2")]
        public void ThenTheScenarioContextDeliversTheValueForKeyInStepClass(string value, string key)
        {
            this.scenarioContext.Get<string>(key).Should().Be(value);
        }

        [Then(@"the feature context delivers the value '(.*)' for key '(.*)' in StepClass2")]
        public void ThenTheFeatureContextDeliversTheValueForKeyInStepClass(string value, string key)
        {
            this.featureContext.Get<string>(key).Should().Be(value);
        }
    }
}