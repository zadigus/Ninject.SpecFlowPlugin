namespace SpecFlowPlugin.Acceptance.TestClasses
{
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    public sealed class SingletonDisposableFeatureDependency2 : ISingletonDisposableFeatureDependency2
    {
        private readonly TestThreadContext testThreadContext;

        public SingletonDisposableFeatureDependency2(TestThreadContext testThreadContext)
        {
            this.testThreadContext = testThreadContext;
        }

        public void Dispose()
        {
            // to check that this object has been disposed, we need to be outside of the feature,
            // since the kernel disposer hook runs as the very last AfterFeature hook;
            // for that reason, we set the flag in the test thread context
            if (this.testThreadContext.Get<bool>(ContextKeys.MustDisposeSingletonFeatureDependency2))
            {
                this.testThreadContext.Save(
                    true,
                    ContextKeys.SingletonDisposableFeatureDependency2IsDisposed);
            }
        }
    }
}