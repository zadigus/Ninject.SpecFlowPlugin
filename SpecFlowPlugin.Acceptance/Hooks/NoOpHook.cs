namespace SpecFlowPlugin.Acceptance.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class NoOpHook
    {
        // without this hook, there would be no binding in this assembly; consequently,
        // the binding registry would not list it as a binding assembly, therefore
        // making it impossible to find the Scenario / FeatureDependencies attribute,
        // since it is defined in this assembly
        [BeforeScenario]
        public void OnlyHereSoThatTheBindingRegistryListsThisAssemblyToo()
        {
        }
    }
}