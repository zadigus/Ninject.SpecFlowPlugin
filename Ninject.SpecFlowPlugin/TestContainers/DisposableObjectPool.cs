namespace Ninject.SpecFlowPlugin.TestContainers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;

    public class DisposableObjectPool : IDisposable
    {
        private readonly HashSet<IDisposable> pool = new HashSet<IDisposable>();

        private bool alreadyDisposed;

        public void Add(IEnumerable<object> instances)
        {
            var disposableTypes = instances.OfType<IDisposable>().Where(IsPoolable);
            this.pool.UnionWith(disposableTypes);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.alreadyDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                foreach (var disposableObject in this.pool)
                {
                    disposableObject.Dispose();
                }
            }

            this.alreadyDisposed = true;
        }

        private static bool IsPoolable(IDisposable x)
        {
            return !(x is IKernel) && !(x is ScenarioContext) && !(x is FeatureContext) && !(x is TestThreadContext);
        }
    }
}