namespace Ninject.SpecFlowPlugin.Acceptance.StepDefinitions
{
    using Castle.Core.Internal;
    using FluentAssertions;
    using Ninject.SpecFlowPlugin.Acceptance.TestClasses;
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
        public void GivenIInjectTransientScenarioDependencyInTheBindingClassStepClass1()
        {
            this.transientScenarioDependency.Should().BeOfType<TransientScenarioDependency>();
        }

        [Given(@"I have injected TransientFeatureDependency in the binding class StepClass1")]
        public void GivenIInjectTransientFeatureDependencyInTheBindingClassStepClass1()
        {
            this.transientFeatureDependency.Should().BeOfType<TransientFeatureDependency>();
        }

        [Given(@"the property MyProp of TransientScenarioDependency has value '(.*)' in StepClass1")]
        public void GivenThePropertyMyPropOfTransientScenarioDependencyHasValueInStepClass1(string value)
        {
            this.transientScenarioDependency.MyProp = value;
        }

        [Given(@"the property MyProp of TransientFeatureDependency has value '(.*)' in StepClass1")]
        public void GivenThePropertyMyPropOfTransientFeatureDependencyHasValueInStepClass1(string value)
        {
            this.transientFeatureDependency.MyProp = value;
        }

        [Given(@"I have injected SingletonScenarioDependency in the binding class StepClass1")]
        public void GivenIInjectSingletonScenarioDependencyInTheBindingClassStepClass1()
        {
            this.singletonScenarioDependency.Should().BeOfType<SingletonScenarioDependency>();
        }

        [Given(@"I have injected SingletonFeatureDependency in the binding class StepClass1")]
        public void GivenIInjectSingletonFeatureDependencyInTheBindingClassStepClass1()
        {
            this.singletonFeatureDependency.Should().BeOfType<SingletonFeatureDependency>();
        }

        [Given(@"I have injected the scenario context in the binding class StepClass1")]
        [When(@"I inject the scenario context in the binding class StepClass1")]
        public void GivenIHaveInjectedTheScenarioContextInTheBindingClassStepClass1()
        {
            this.scenarioContext.Should().BeOfType<ScenarioContext>();
        }

        [Given(@"the scenario context has no value corresponding to key '(.*)' in StepClass1")]
        public void GivenTheScenarioContextHasNoValueCorrespondingToKeyInStepClass1(string key)
        {
            this.scenarioContext.ContainsKey(key).Should().BeFalse();
        }

        [Given(@"I have injected the feature context in the binding class StepClass1")]
        [When(@"I inject the feature context in the binding class StepClass1")]
        public void GivenIHaveInjectedTheFeatureContextInTheBindingClassStepClass1()
        {
            this.featureContext.Should().BeOfType<FeatureContext>();
        }

        [Given(@"the feature context has no value corresponding to key '(.*)' in StepClass1")]
        public void GivenTheFeatureContextHasNoValueCorrespondingToKeyInStepClass1(string key)
        {
            this.featureContext.ContainsKey(key).Should().BeFalse();
        }

        [When(@"I set property MyProp of SingletonScenarioDependency to '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonScenarioDependencyToInStepClass1(string value)
        {
            this.singletonScenarioDependency.MyProp = value;
        }

        [When(@"I set property MyProp of SingletonFeatureDependency to '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonFeatureDependencyToInStepClass1(string value)
        {
            this.singletonFeatureDependency.MyProp = value;
        }

        [When(@"I associate the value '(.*)' to key '(.*)' of the scenario context in StepClass1")]
        public void WhenIAssociateTheValueToKeyOfTheScenarioContextInStepClass1(string value, string key)
        {
            this.scenarioContext.Set(value, key);
        }

        [When(@"I associate the value '(.*)' to key '(.*)' of the feature context in StepClass1")]
        public void WhenIAssociateTheValueToKeyOfTheFeatureContextInStepClass1(string value, string key)
        {
            this.featureContext.Set(value, key);
        }

        [When(@"I remove the key '(.*)' from the feature context in StepClass1")]
        public void WhenIRemoveTheKeyFromTheFeatureContextInStepClass1(string key)
        {
            var removedKey = this.featureContext.Remove(key);
            this.featureContext.Save(removedKey, ContextKeys.RemovedKey);
        }

        [When(@"I set property MyProp of TransientFeatureDependency to value '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfTransientFeatureDependencyToValueInStepClass1(string myPropValue)
        {
            this.transientFeatureDependency.MyProp = myPropValue;
        }

        [When(@"I set property MyProp of TransientScenarioDependency to value '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfTransientScenarioDependencyToValueInStepClass1(string myPropValue)
        {
            this.transientScenarioDependency.MyProp = myPropValue;
        }

        [When(@"I set property MyProp of SingletonScenarioDependency to value '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonScenarioDependencyToValueInStepClass1(string myPropValue)
        {
            this.singletonScenarioDependency.MyProp = myPropValue;
        }

        [When(@"I set property MyProp of SingletonFeatureDependency to value '(.*)' in StepClass1")]
        public void WhenISetPropertyMyPropOfSingletonFeatureDependencyToValueInStepClass1(string writtenValue)
        {
            this.singletonFeatureDependency.MyProp = writtenValue;
        }

        [Then(@"the property MyProp of TransientScenarioDependency has value '(.*)' in StepClass1")]
        public void ThenThePropertyMyPropOfTransientScenarioDependencyHasValueInStepClass1(string value)
        {
            this.transientScenarioDependency.MyProp.Should().Be(value);
        }

        [Then(@"the property MyProp of TransientFeatureDependency has value '(.*)' in StepClass1")]
        public void ThenThePropertyMyPropOfTransientFeatureDependencyHasValueInStepClass1(string value)
        {
            this.transientFeatureDependency.MyProp.Should().Be(value);
        }

        [Then(@"the scenario context has no value corresponding to key '(.*)' in StepClass1")]
        public void ThenTheScenarioContextHasNoValueCorrespondingToKeyInStepClass1(string key)
        {
            this.scenarioContext.Should().NotContainKey(key);
        }

        [Then(@"the feature context has value '(.*)' corresponding to key '(.*)' in StepClass1")]
        public void ThenTheFeatureContextHasValueCorrespondingToKeyInStepClass1(string value, string key)
        {
            this.featureContext.Get<string>(key).Should().Be(value);
        }

        [Then(@"the property MyProp of TransientFeatureDependency has no value in StepClass1")]
        public void ThenThePropertyMyPropOfTransientFeatureDependencyHasNoValueInStepClass1()
        {
            this.transientFeatureDependency.MyProp.Should().BeNullOrEmpty();
        }

        [Then(@"the property MyProp of TransientScenarioDependency has no value in StepClass1")]
        public void ThenThePropertyMyPropOfTransientScenarioDependencyHasNoValueInStepClass1()
        {
            this.transientScenarioDependency.MyProp.Should().BeNullOrEmpty();
        }

        [Then(@"the property MyProp of SingletonScenarioDependency has no value in StepClass1")]
        public void ThenThePropertyMyPropOfSingletonScenarioDependencyHasNoValueInStepClass1()
        {
            this.singletonScenarioDependency.MyProp.Should().BeNullOrEmpty();
        }

        [Then(@"the property MyProp of SingletonFeatureDependency has value '(.*)' in StepClass1")]
        public void ThenThePropertyMyPropOfSingletonFeatureDependencyHasValueInStepClass1(string readValue)
        {
            if (readValue.IsNullOrEmpty())
            {
                this.singletonFeatureDependency.MyProp.Should().BeNullOrEmpty();
            }
            else
            {
                this.singletonFeatureDependency.MyProp.Should().Be(readValue);
            }
        }
    }
}