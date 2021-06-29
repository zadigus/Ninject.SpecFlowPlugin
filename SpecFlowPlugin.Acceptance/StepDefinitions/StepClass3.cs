namespace SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class StepClass3
    {
        private readonly FeatureContext featureContext;

        public StepClass3(FeatureContext featureContext)
        {
            this.featureContext = featureContext;
        }

        public string MyProp { get; set; }

        [Given(@"I define the key '(.*)' in the feature context")]
        public void GivenIDefineTheKeyInTheFeatureContext(string key)
        {
            this.featureContext.Set("whatever", key);
        }

        [Then(@"if the feature context contains the key '(.*)' then it should not contain the key '(.*)'")]
        public void ThenIfTheFeatureContextContainsTheKeyThenItShouldNotContainTheKey(string keyIf, string unwantedKey)
        {
            if (this.featureContext.ContainsKey(keyIf))
            {
                this.featureContext.Should().NotContainKey(unwantedKey);
            }
        }

        [Then(@"if the feature context does not contain the key '(.*)' then it should contain the key '(.*)'")]
        public void ThenIfTheFeatureContextDoesNotContainTheKeyThenItShouldContainTheKey(string keyIf, string wantedKey)
        {
            if (!this.featureContext.ContainsKey(keyIf))
            {
                this.featureContext.Should().ContainKey(wantedKey);
            }
        }
    }
}