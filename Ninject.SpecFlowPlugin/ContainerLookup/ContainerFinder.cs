namespace Ninject.SpecFlowPlugin.ContainerLookup
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.Extensions;
    using TechTalk.SpecFlow.Bindings;

    public abstract class ContainerFinder<TDependenciesAttribute>
        where TDependenciesAttribute : ContainerConfigurationAttribute
    {
        private readonly IBindingRegistry bindingRegistry;

        protected ContainerFinder(IBindingRegistry bindingRegistry)
        {
            this.bindingRegistry = bindingRegistry;
        }

        public Action<IKernel> SetupContainerFunc()
        {
            return this.FindSetupContainer();
        }

        protected virtual Action<IKernel> FindSetupContainer()
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
                        ContainerConfiguratorChecker<TDependenciesAttribute>.CheckMethodSignature(methodInfo);

                        return kernel => methodInfo.Invoke(null, new object[] { kernel });
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