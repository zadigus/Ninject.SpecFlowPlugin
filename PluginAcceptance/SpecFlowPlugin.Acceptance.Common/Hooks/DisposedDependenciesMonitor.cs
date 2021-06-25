namespace SpecFlowPlugin.Acceptance.Common.Hooks
{
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public sealed class DisposedDependenciesMonitor
    {
        [AfterFeature("check-disposed-after-scenario")]
        public static void AfterFeature(FeatureContext featureContext)
        {
            var disposableInstanceIsDisposedKey = ContextKeys.DisposableInstanceIsDisposed.ToString();
            var isInstanceDisposed = featureContext.ContainsKey(disposableInstanceIsDisposedKey) &&
                                     featureContext.Get<bool>(disposableInstanceIsDisposedKey);
            isInstanceDisposed.Should().BeTrue();
        }

        [AfterTestRun]
        public static void AfterTest(TestThreadContext testThreadContext)
        {
            // here we cannot proceed the same way as for scenario dependencies, because the
            // AfterTestRun does not support tag filtering, therefore this little hack
            var mustDisposeFeatureDependencyKey = ContextKeys.MustDisposeFeatureDependency.ToString();
            var mustDisposeFeatureDependency = testThreadContext.ContainsKey(mustDisposeFeatureDependencyKey) &&
                                               testThreadContext.Get<bool>(mustDisposeFeatureDependencyKey);
            if (mustDisposeFeatureDependency)
            {
                var featureDependencyDisposed = testThreadContext.Get<bool>(mustDisposeFeatureDependencyKey);
                featureDependencyDisposed.Should().BeTrue();
            }
        }
    }
}