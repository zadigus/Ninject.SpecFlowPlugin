namespace SpecFlowPlugin.Acceptance.Common.TestClasses
{
    using TechTalk.SpecFlow;

    public sealed class TransientDisposableFeatureDependency : ITransientDisposableFeatureDependency
    {
        private readonly TestThreadContext testThreadContext;

        public TransientDisposableFeatureDependency(TestThreadContext testThreadContext)
        {
            this.testThreadContext = testThreadContext;
        }

        public void Dispose()
        {
            // to check that this object has been disposed, we need to be outside of the feature,
            // since the kernel disposer hook runs as the very last AfterFeature hook;
            // for that reason, we check it in the AfterTestRun hook, where the object has already
            // been disposed, therefore it should not be possible to access it anymore, as any of its
            // properties, hence the flag stored in the test thread context
            this.testThreadContext.Set(true, ContextKeys.DisposableInstanceIsDisposed.ToString());
        }
    }
}