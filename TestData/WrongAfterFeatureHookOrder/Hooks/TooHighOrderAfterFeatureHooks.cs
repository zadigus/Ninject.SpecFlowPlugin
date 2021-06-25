namespace WrongAfterFeatureHookOrder.Hooks
{
    using SpecFlowPluginBase;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class TooHighOrderAfterFeatureHooks
    {
        [AfterFeature(Order = Constants.ContainerDisposerHookOrder + 1)]
        public static void AfterFeatureWithTooHighOrder()
        {
            // do nothing
        }

        public void ThisMethodIsNecessaryToMockIBindingRegistry()
        {
        }
    }
}