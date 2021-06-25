namespace WrongInputArgTypeFeatureDependencies.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class NoOpHook
    {
        public void ThisMethodIsNecessaryToMockIBindingRegistry()
        {
        }
    }
}