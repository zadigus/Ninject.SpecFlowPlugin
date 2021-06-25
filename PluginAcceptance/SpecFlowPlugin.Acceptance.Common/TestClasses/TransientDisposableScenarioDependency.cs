namespace SpecFlowPlugin.Acceptance.Common.TestClasses
{
    using TechTalk.SpecFlow;

    public sealed class TransientDisposableScenarioDependency : ITransientDisposableScenarioDependency
    {
        private readonly FeatureContext featureContext;

        public TransientDisposableScenarioDependency(FeatureContext featureContext)
        {
            this.featureContext = featureContext;
        }

        public void Dispose()
        {
            // to check that this object has been disposed, we need to be outside of the scenario,
            // since the kernel disposer hook runs as the very last AfterScenario hook;
            // for that reason, we check it in the AfterFeature hook, where the object has already
            // been disposed, therefore it should not be possible to access it anymore, as any of its
            // properties, hence the flag stored in the feature context instead of the use of a Disposed
            // property
            this.featureContext.Set(true, ContextKeys.DisposableInstanceIsDisposed.ToString());
        }
    }
}