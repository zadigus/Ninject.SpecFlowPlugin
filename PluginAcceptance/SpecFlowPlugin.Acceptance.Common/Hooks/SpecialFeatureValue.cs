namespace SpecFlowPlugin.Acceptance.Common.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class SpecialFeatureValue
    {
        [BeforeFeature("special-feature-value")]
        public static void SetSpecialFeatureValue(FeatureContext featureContext)
        {
            featureContext.Set("MyValueSetInFeatureHook", "MyKeySetInFeatureHook");
        }
    }
}