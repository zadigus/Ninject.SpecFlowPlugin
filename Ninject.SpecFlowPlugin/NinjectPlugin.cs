using Ninject.SpecFlowPlugin;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(NinjectPlugin))]

namespace Ninject.SpecFlowPlugin
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using BoDi;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.ContainerLookup;
    using Ninject.SpecFlowPlugin.TestContainers;
    using Ninject.Syntax;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Infrastructure;
    using TechTalk.SpecFlow.Plugins;
    using TechTalk.SpecFlow.UnitTestProvider;

    public class NinjectPlugin : IRuntimePlugin
    {
        private static readonly object RegistrationLock = new object();

        [SuppressMessage(
            "Microsoft.Maintainability",
            "CA1506:AvoidExcessiveClassCoupling",
            Justification = "we need it like this")]
        [SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "the kernel is disposed after feature")]
        public void Initialize(
            RuntimePluginEvents runtimePluginEvents,
            RuntimePluginParameters runtimePluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            unitTestProviderConfiguration.CheckNullArgument(nameof(unitTestProviderConfiguration));
            runtimePluginParameters.CheckNullArgument(nameof(runtimePluginParameters));
            runtimePluginEvents.CheckNullArgument(nameof(runtimePluginEvents));

            runtimePluginEvents.ConfigurationDefaults += (sender, args) =>
            {
                var pluginAssemblyName = Assembly.GetExecutingAssembly().GetName();
                args.SpecFlowConfiguration.AdditionalStepAssemblies.Add(pluginAssemblyName.Name);
            };

            runtimePluginEvents.CustomizeGlobalDependencies += (sender, args) =>
            {
                // temporary fix for CustomizeGlobalDependencies called multiple times
                // see https://github.com/techtalk/SpecFlow/issues/948
                if (!args.ObjectContainer.IsRegistered<ContainerFinder<ScenarioDependenciesAttribute>>())
                {
                    // an extra lock to ensure that there are not two super fast threads re-registering the same stuff
                    lock (RegistrationLock)
                    {
                        if (!args.ObjectContainer.IsRegistered<ContainerFinder<ScenarioDependenciesAttribute>>())
                        {
                            args.ObjectContainer.RegisterTypeAs<NinjectTestObjectResolver, ITestObjectResolver>();
                            args.ObjectContainer
                                .RegisterTypeAs<TestThreadContainerFinder,
                                    ContainerFinder<TestThreadDependenciesAttribute>>();
                            args.ObjectContainer
                                .RegisterTypeAs<FeatureContainerFinder,
                                    ContainerFinder<FeatureDependenciesAttribute>>();
                            args.ObjectContainer
                                .RegisterTypeAs<ScenarioContainerFinder,
                                    ContainerFinder<ScenarioDependenciesAttribute>>();
                        }
                    }

                    // workaround for parallel execution issue - this should be rather a feature in BoDi?
                    args.ObjectContainer.Resolve<ContainerFinder<TestThreadDependenciesAttribute>>();
                    args.ObjectContainer.Resolve<ContainerFinder<FeatureDependenciesAttribute>>();
                    args.ObjectContainer.Resolve<ContainerFinder<ScenarioDependenciesAttribute>>();
                }
            };

            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterFactoryAs(
                    () =>
                    {
                        var objectContainer = args.ObjectContainer;
                        var containerFinder =
                            objectContainer.Resolve<ContainerFinder<TestThreadDependenciesAttribute>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = CreateAcceptanceTestKernel(objectContainer);
                        RegisterSpecflowTestThreadDependencies(objectContainer, container);
                        setupContainer?.Invoke(container);

                        return container;
                    });
            };

            runtimePluginEvents.CustomizeFeatureDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterFactoryAs(
                    () =>
                    {
                        var objectContainer = args.ObjectContainer;
                        var containerFinder = objectContainer.Resolve<ContainerFinder<FeatureDependenciesAttribute>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = CreateAcceptanceTestKernel(objectContainer);
                        RegisterSpecflowFeatureDependencies(objectContainer, container);
                        setupContainer?.Invoke(container);
                        return container;
                    });
            };

            runtimePluginEvents.CustomizeScenarioDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterFactoryAs(
                    () =>
                    {
                        var objectContainer = args.ObjectContainer;
                        var containerFinder = objectContainer.Resolve<ContainerFinder<ScenarioDependenciesAttribute>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = CreateAcceptanceTestKernel(objectContainer);
                        RegisterSpecflowScenarioDependencies(objectContainer, container);
                        setupContainer(container);
                        return container;
                    });
            };
        }

        private static IKernel CreateAcceptanceTestKernel(ObjectContainer objectContainer)
        {
            var baseObjectContainer = objectContainer.BaseContainer;

            if (baseObjectContainer.IsRegistered<IKernel>())
            {
                var baseKernel = baseObjectContainer.Resolve<IKernel>();
                return new ChildAcceptanceTestKernel(baseKernel);
            }

            return new AcceptanceTestKernel();
        }

        private static void RegisterSpecflowScenarioDependencies(
            IObjectContainer objectContainer,
            IBindingRoot container)
        {
            container.Bind<IObjectContainer>().ToConstant(objectContainer);
            container.Bind<ScenarioContext>().ToMethod(ctx => objectContainer.Resolve<ScenarioContext>());
        }

        private static void RegisterSpecflowFeatureDependencies(
            IObjectContainer objectContainer,
            IBindingRoot container)
        {
            container.Bind<IObjectContainer>().ToConstant(objectContainer);
            container.Bind<FeatureContext>().ToMethod(ctx => objectContainer.Resolve<FeatureContext>());
        }

        private static void RegisterSpecflowTestThreadDependencies(
            IObjectContainer objectContainer,
            IBindingRoot container)
        {
            container.Bind<IObjectContainer>().ToConstant(objectContainer);
            container.Bind<TestThreadContext>().ToMethod(ctx => objectContainer.Resolve<TestThreadContext>());
        }
    }
}