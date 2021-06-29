namespace SpecFlowPluginBase
{
    using System;
    using BoDi;
    using TechTalk.SpecFlow.Infrastructure;

    public abstract class TestObjectResolver<TContainerType> : ITestObjectResolver
    {
        public object ResolveBindingInstance(Type bindingType, IObjectContainer container)
        {
            container.CheckNullArgument(nameof(container));
            bindingType.CheckNullArgument(nameof(bindingType));

            var userContainer = container.Resolve<TContainerType>();
            return this.ResolveFromUserContainer(userContainer, bindingType);
        }

        protected abstract object ResolveFromUserContainer(TContainerType userContainer, Type bindingType);
    }
}