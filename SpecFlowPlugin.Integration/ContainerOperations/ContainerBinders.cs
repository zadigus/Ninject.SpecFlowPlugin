namespace SpecFlowPlugin.Integration.ContainerOperations
{
    using System;
    using System.Collections.Generic;
    using Ninject;

    public static class ContainerBinders
    {
        // TODO: this is not OCP-compliant
        private static Dictionary<Type, Action<object, Type, Type>> Binders { get; } =
            new Dictionary<Type, Action<object, Type, Type>>
            {
                {
                    typeof(IKernel),
                    (container, serviceType, serviceInterfaceType) =>
                        (container as IKernel).Bind(serviceInterfaceType).To(serviceType)
                }
            };

        public static Action<object, Type, Type> GetBinder<TContainerType>()
            where TContainerType : class
        {
            return Binders[typeof(TContainerType)];
        }
    }
}