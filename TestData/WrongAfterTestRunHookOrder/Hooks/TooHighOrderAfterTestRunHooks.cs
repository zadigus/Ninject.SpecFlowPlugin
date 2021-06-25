namespace WrongAfterTestRunHookOrder.Hooks
{
    using Ninject.SpecFlowPlugin;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterTestRunHooks
    {
        [AfterTestRun(Order = Constants.KernelDisposerOrder + 1)]
        public static void AfterTestRunWithTooHighOrder()
        {
            // do nothing
        }

        public void ThisMethodIsNecessaryToMockIBindingRegistry()
        {
        }
    }
}