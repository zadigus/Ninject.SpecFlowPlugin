namespace Ninject.SpecFlowPlugin.TestContainers
{
    using System.Collections.Generic;
    using System.Linq;
    using Ninject.Activation;
    using Ninject.Extensions.ChildKernel;
    using Ninject.Modules;
    using Ninject.Syntax;
    using SpecFlowPluginBase;

    public class ChildAcceptanceTestKernel : ChildKernel
    {
        private readonly DisposableObjectPool<IKernel> pool = new DisposableObjectPool<IKernel>();

        public ChildAcceptanceTestKernel(IResolutionRoot parent, params INinjectModule[] modules)
            : base(parent, modules)
        {
        }

        /// <summary>
        ///     This is the only resolution method to override because NinjectTestObjectResolver only calls IKernel.Get,
        ///     which is an extension method on IResolutionRoot that calls Ninject.ResolutionExtensions.GetResolutionIterator
        ///     which creates an IRequest and calls IResolutionRoot.Resolve(IRequest).
        /// </summary>
        /// <param name="request">The request to resolve.</param>
        /// <returns>An enumerator of instances that match the request.</returns>
        public override IEnumerable<object> Resolve(IRequest request)
        {
            var instances = base.Resolve(request).ToList();
            this.pool.Add(instances);
            return instances;
        }

        /// <summary>
        ///     To comply with the SpecFlow documentation
        ///     https://docs.specflow.org/projects/specflow/en/latest/Bindings/Context-Injection.html#context-injection
        ///     we need to dispose the object listed in the pool of disposable objects.
        /// </summary>
        /// <param name="disposing"><c>True</c> if called manually, <c>False</c> if called by GC.</param>
        public override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.pool.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}