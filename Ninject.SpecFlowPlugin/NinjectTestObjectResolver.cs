namespace Ninject.SpecFlowPlugin
{
    using System;
    using BoDi;
    using TechTalk.SpecFlow.Infrastructure;

    public class NinjectTestObjectResolver : ITestObjectResolver
    {
        public object ResolveBindingInstance(Type bindingType, IObjectContainer container)
        {
            container.CheckNullArgument(nameof(container));
            bindingType.CheckNullArgument(nameof(bindingType));

            var kernel = container.Resolve<IKernel>();
            return kernel.Get(bindingType);
        }
    }
}