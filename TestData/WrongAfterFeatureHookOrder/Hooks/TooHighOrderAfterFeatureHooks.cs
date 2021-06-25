namespace WrongAfterFeatureHookOrder.Hooks
{
    using Ninject.SpecFlowPlugin;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterFeatureHooks
    {
        [AfterFeature(Order = Constants.KernelDisposerOrder + 1)]
        public static void AfterFeatureWithTooHighOrder()
        {
            // do nothing
        }

        public void ThisMethodIsNecessaryToMockIBindingRegistry()
        {
        }
    }
}