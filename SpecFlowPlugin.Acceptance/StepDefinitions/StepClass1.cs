namespace SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using SpecFlowPlugin.Acceptance.TestClasses;
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class StepClass1
    {
        private readonly FeatureContext featureContext;

        private readonly ScenarioContext scenarioContext;

        private readonly ISingletonFeatureDependency singletonFeatureDependency;

        private readonly ISingletonScenarioDependency singletonScenarioDependency;

        private readonly ITransientFeatureDependency transientFeatureDependency;

        private readonly ITransientScenarioDependency transientScenarioDependency;

        public StepClass1(
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

        [Given(@"I have injected TransientScenarioDependency in the binding class StepClass1")]
        public void GivenIInjectTransientScenarioDependencyInTheBindingClassStepClass()
        {
            this.transientScenarioDependency.Should().BeOfType<TransientScenarioDependency>();
        }

        [Given(@"I have injected TransientFeatureDependency in the binding class StepClass1")]
        public void GivenIInjectTransientFeatureDependencyInTheBindingClassStepClass()
        {
            this.transientFeatureDependency.Should().BeOfType<TransientFeatureDependency>();
        }

        [Given(@"the property MyProp of TransientScenarioDependency has value '(.*)' in StepClass1")]
        public void GivenThePropertyMyPropOfTransientScenarioDependencyHasValueInStepClass(string value)
        {
            this.transientScenarioDependency.MyProp = value;
        }

        [Given(@"the property MyProp of TransientFeatureDependency has value '(.*)' in StepClass1")]
        public void GivenThePropertyMyPropOfTransientFeatureDependencyHasValueInStepClass(string value)
        {
            this.transientFeatureDependency.MyProp = value;
        }

        [Given(@"I have injected SingletonScenarioDependency in the binding class StepClass1")]
        public void GivenIInjectSingletonScenarioDependencyInTheBindingClassStepClass()
        {
            this.singletonScenarioDependency.Should().BeOfType<SingletonScenarioDependency>();
        }

        [Given(@"I have injected SingletonFeatureDependency in the binding class StepClass1")]
        public void GivenIInjectSingletonFeatureDependencyInTheBindingClassStepClass()
        {
            this.singletonFeatureDependency.Should().BeOfType<SingletonFeatureDependency>();
        }

        [Given(@"I have injected the scenario context in the binding class StepClass1")]
        [When(@"I inject the scenario context in the binding class StepClass1")]
        public void GivenIHaveInjectedTheScenarioContextInTheBindingClassStepClass()
        {
            this.scenarioContext.Should().BeOfType<ScenarioContext>();
        }

        [Given(@"the scenario context has no value corresponding to key '(.*)' in StepClass1")]
        public void GivenTheScenarioContextHasNoValueCorrespondingToKeyInStepClass(string key)
        {
            this.scenarioContext.ContainsKey(key).Should().BeFalse();
        }

        [Given(@"I have injected the feature context in the binding class StepClass1")]
        [When(@"I inject the feature context in the binding class StepClass1")]
        public void GivenIHaveInjectedTheFeatureContextInTheBindingClassStepClass()
        {
            this.featureContext.Should().BeOfType<FeatureContext>();
        }

        [Given(@"the feature context has no value corresponding to key '(.*)' in StepClass1")]
        public void GivenTheFeatureContextHasNoValueCorrespondingToKeyInStepClass(string key)
        {
            this.featureContext.ContainsKey(key).Should().BeFalse();
        }

        [When(@"I set property MyProp of SingletonScenarioDependency to '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonScenarioDependencyToInStepClass(string value)
        {
            this.singletonScenarioDependency.MyProp = value;
        }

        [When(@"I set property MyProp of SingletonFeatureDependency to '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonFeatureDependencyToInStepClass(string value)
        {
            this.singletonFeatureDependency.MyProp = value;
        }

        [When(@"I associate the value '(.*)' to key '(.*)' of the scenario context in StepClass1")]
        public void WhenIAssociateTheValueToKeyOfTheScenarioContextInStepClass(string value, string key)
        {
            this.scenarioContext.Set(value, key);
        }

        [When(@"I associate the value '(.*)' to key '(.*)' of the feature context in StepClass1")]
        public void WhenIAssociateTheValueToKeyOfTheFeatureContextInStepClass(string value, string key)
        {
            this.featureContext.Set(value, key);
        }

        [When(@"I remove the key '(.*)' from the feature context in StepClass1")]
        public void WhenIRemoveTheKeyFromTheFeatureContextInStepClass(string key)
        {
            var removedKey = this.featureContext.Remove(key);
            this.featureContext.Save(removedKey, ContextKeys.RemovedKey);
        }

        [Then(@"the property MyProp of TransientScenarioDependency has value '(.*)' in StepClass1")]
        public void ThenThePropertyMyPropOfTransientScenarioDependencyHasValueInStepClass(string value)
        {
            this.transientScenarioDependency.MyProp.Should().Be(value);
        }

        [Then(@"the property MyProp of TransientFeatureDependency has value '(.*)' in StepClass1")]
        public void ThenThePropertyMyPropOfTransientFeatureDependencyHasValueInStepClass(string value)
        {
            this.transientFeatureDependency.MyProp.Should().Be(value);
        }

        [Then(@"the scenario context has no value corresponding to key '(.*)' in StepClass1")]
        public void ThenTheScenarioContextHasNoValueCorrespondingToKeyInStepClass(string key)
        {
            this.scenarioContext.Should().NotContainKey(key);
        }

        [Then(@"the feature context has value '(.*)' corresponding to key '(.*)' in StepClass1")]
        public void ThenTheFeatureContextHasValueCorrespondingToKeyInStepClass(string value, string key)
        {
            this.featureContext.Get<string>(key).Should().Be(value);
        }

        [Then(@"the key has been removed from the feature context")]
        public void ThenTheKeyHasBeenRemoved()
        {
            var removedKey = this.featureContext.Get<bool>(ContextKeys.RemovedKey);
            removedKey.Should().BeTrue();
        }
    }
}