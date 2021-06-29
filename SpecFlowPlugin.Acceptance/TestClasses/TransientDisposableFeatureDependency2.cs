namespace SpecFlowPlugin.Acceptance.TestClasses
{
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    public sealed class TransientDisposableFeatureDependency2 : ITransientDisposableFeatureDependency2
    {
        private readonly TestThreadContext testThreadContext;

        public TransientDisposableFeatureDependency2(TestThreadContext testThreadContext)
        {
            this.testThreadContext = testThreadContext;
        }

        public void Dispose()
        {
            // to check that this object has been disposed, we need to be outside of the feature,
            // since the kernel disposer hook runs as the very last AfterFeature hook;
            // for that reason, we set the flag in the test thread context
            if (this.testThreadContext.Get<bool>(ContextKeys.MustDisposeTransientFeatureDependency2))
            {
                this.testThreadContext.Save(
                    true,
                    ContextKeys.TransientDisposableFeatureDependency2IsDisposed);
            }
        }
    }
}