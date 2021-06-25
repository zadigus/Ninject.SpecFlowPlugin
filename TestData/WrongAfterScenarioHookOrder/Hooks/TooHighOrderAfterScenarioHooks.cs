namespace WrongAfterScenarioHookOrder.Hooks
{
    using Ninject.SpecFlowPlugin;
    using SpecFlowPluginBase;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterScenarioHooks
    {
        [AfterScenario(Order = Constants.ContainerDisposerHookOrder + 1)]
        public void AfterScenarioWithTooHighOrder()
        {
            // do nothing
        }
    }
}