namespace WrongAfterTestRunHookOrder.Hooks
{
    using Ninject.SpecFlowPlugin;
    using SpecFlowPluginBase;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterTestRunHooks
    {
        [AfterTestRun(Order = Constants.ContainerDisposerHookOrder + 1)]
        public static void AfterTestRunWithTooHighOrder()
        {
            // do nothing
        }

        public void ThisMethodIsNecessaryToMockIBindingRegistry()
        {
        }
    }
}