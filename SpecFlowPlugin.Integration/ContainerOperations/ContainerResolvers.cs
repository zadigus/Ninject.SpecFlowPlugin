namespace SpecFlowPlugin.Integration.ContainerOperations
{
    using System;
    using System.Collections.Generic;
    using Ninject;

    public static class ContainerResolvers
    {
        // TODO: this is not OCP-compliant
        private static Dictionary<Type, Func<object, Type, object>> Resolvers { get; } =
            new Dictionary<Type, Func<object, Type, object>>
            {
                { typeof(IKernel), (container, serviceType) => (container as IKernel).Get(serviceType) }
            };

        public static Func<object, Type, object> GetResolver<TContainerType>()
            where TContainerType : class
        {
            return Resolvers[typeof(TContainerType)];
        }
    }
}