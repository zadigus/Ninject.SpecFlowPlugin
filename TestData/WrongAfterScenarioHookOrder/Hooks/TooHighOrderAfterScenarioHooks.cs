namespace WrongAfterScenarioHookOrder.Hooks
{
    using Ninject.SpecFlowPlugin;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterScenarioHooks
    {
        [AfterScenario(Order = Constants.KernelDisposerOrder + 1)]
        public void AfterScenarioWithTooHighOrder()
        {
            // do nothing
        }
    }
}