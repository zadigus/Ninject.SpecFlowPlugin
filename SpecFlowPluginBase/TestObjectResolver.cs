namespace SpecFlowPluginBase
{
    using System;
    using BoDi;
    using TechTalk.SpecFlow.Infrastructure;

    public abstract class TestObjectResolver<TContainerType> : ITestObjectResolver
    {
        public abstract object ResolveBindingInstance(Type bindingType, IObjectContainer container);
    }
}