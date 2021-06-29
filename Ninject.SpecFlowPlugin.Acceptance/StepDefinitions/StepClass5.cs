namespace Ninject.SpecFlowPlugin.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class StepClass5
    {
        private readonly StepClass3 stepClass3;

        public StepClass5(StepClass3 stepClass3)
        {
            this.stepClass3 = stepClass3;
        }

        [Given(@"I have injected StepClass3 in StepClass5")]
        public void GivenIInjectStepClassInStepClass()
        {
            this.stepClass3.Should().NotBeNull();
        }

        [Then(@"the property MyProp of StepClass3 has value '(.*)' in StepClass5")]
        public void ThenThePropertyMyPropOfStepClassHasValueInStepClass(string value)
        {
            this.stepClass3.MyProp.Should().Be(value);
        }
    }
}