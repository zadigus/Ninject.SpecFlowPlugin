namespace Ninject.SpecFlowPlugin
{
    using System;
    using SpecFlowPluginBase;

    public class NinjectTestObjectResolver : TestObjectResolver<IKernel>
    {
        protected override object ResolveFromUserContainer(IKernel userContainer, Type bindingType)
        {
            return userContainer.Get(bindingType);
        }
    }
}