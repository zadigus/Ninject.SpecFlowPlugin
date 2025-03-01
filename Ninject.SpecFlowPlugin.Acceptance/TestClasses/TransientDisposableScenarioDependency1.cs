﻿namespace Ninject.SpecFlowPlugin.Acceptance.TestClasses
{
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow;

    public sealed class TransientDisposableScenarioDependency1 : ITransientDisposableScenarioDependency1
    {
        private readonly FeatureContext featureContext;

        public TransientDisposableScenarioDependency1(FeatureContext featureContext)
        {
            this.featureContext = featureContext;
        }

        public void Dispose()
        {
            // to check that this object has been disposed, we need to be outside of the scenario,
            // since the kernel disposer hook runs as the very last AfterScenario hook;
            // for that reason, we check it in the scenario context
            if (this.featureContext.Get<bool>(ContextKeys.MustDisposeTransientScenarioDependency1))
            {
                this.featureContext.Save(true, ContextKeys.TransientDisposableScenarioDependency1IsDisposed);
            }
        }
    }
}