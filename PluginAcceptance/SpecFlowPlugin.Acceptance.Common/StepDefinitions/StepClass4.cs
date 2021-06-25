namespace SpecFlowPlugin.Acceptance.Common.StepDefinitions
{
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class StepClass4
    {
        private readonly StepClass3 stepClass3;

        public StepClass4(StepClass3 stepClass3)
        {
            this.stepClass3 = stepClass3;
        }

        [Given(@"I have injected StepClass3 in StepClass4")]
        public void GivenIInjectStepClassInStepClass()
        {
            this.stepClass3.Should().NotBeNull();
        }

        [When(@"I set property MyProp of StepClass3 to '(.*)' in StepClass4")]
        public void WhenISetPropertyMyPropOfStepClassToInStepClass(string value)
        {
            this.stepClass3.MyProp = value;
        }
    }
}