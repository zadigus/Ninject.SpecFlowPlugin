namespace SpecFlowPluginBase.ContainerLookup
{
    using System;
    using System.Linq;
    using System.Reflection;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.Extensions;
    using TechTalk.SpecFlow.Bindings;

    public abstract class ContainerFinder<TDependenciesAttribute, TContainerType>
        where TDependenciesAttribute : ContainerConfigurationAttribute
        where TContainerType : class
    {
        private readonly IBindingRegistry bindingRegistry;

        protected ContainerFinder(IBindingRegistry bindingRegistry)
        {
            this.bindingRegistry = bindingRegistry;
        }

        public Action<TContainerType> SetupContainerFunc()
        {
            return this.FindSetupContainer();
        }

        protected virtual Action<TContainerType> FindSetupContainer()
        {
            var assemblies = this.bindingRegistry.GetBindingAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var methodInfo in type
                        .GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                        .Where(HasDependenciesAttribute))
                    {
                        ContainerConfiguratorChecker<TDependenciesAttribute, TContainerType>.CheckMethodSignature(methodInfo);

                        return container => methodInfo.Invoke(null, new object[] { container });
                    }
                }
            }

            return null;
        }

        private static bool HasDependenciesAttribute(MemberInfo info)
        {
            return Attribute.IsDefined(info, typeof(TDependenciesAttribute));
        }
    }
}