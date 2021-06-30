namespace SpecFlowPlugin.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using BoDi;
    using FluentAssertions;
    using Moq;
    using Ninject;
    using Ninject.SpecFlowPlugin;
    using NUnit.Framework;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.ContainerLookup;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Bindings;
    using TechTalk.SpecFlow.Bindings.Reflection;
    using TechTalk.SpecFlow.Configuration;
    using TechTalk.SpecFlow.Infrastructure;
    using TechTalk.SpecFlow.Plugins;
    using TechTalk.SpecFlow.UnitTestProvider;

    public class BaseFixture<TContainerType>
        where TContainerType : class
    {
        private readonly Func<object, Type, object> resolver;

        private readonly Action<object, Type, Type> binder;

        private readonly SpecFlowConfiguration specFlowConfiguration = ConfigurationLoader.GetDefault();

        private IBindingRegistry bindingRegistry;

        public BaseFixture()
        {
            this.resolver = this.Resolvers[typeof(TContainerType)];
            this.binder = this.Binders[typeof(TContainerType)];
        }

        // TODO: this is not OCP-compliant
        protected Dictionary<Type, Func<object, Type, object>> Resolvers { get; } =
            new Dictionary<Type, Func<object, Type, object>>
            {
                { typeof(IKernel), (container, serviceType) => (container as IKernel).Get(serviceType) }
            };

        protected Dictionary<Type, Action<object, Type, Type>> Binders { get; } =
            new Dictionary<Type, Action<object, Type, Type>>
            {
                {
                    typeof(IKernel),
                    (container, serviceType, serviceInterfaceType) =>
                        (container as IKernel).Bind(serviceInterfaceType).To(serviceType)
                }
            };

        protected ObjectContainer GlobalContainer { get; private set; }

        protected RuntimePluginEvents PluginEvents { get; private set; }

        [SetUp]
        public void SetUpBase()
        {
            this.bindingRegistry = Mock.Of<IBindingRegistry>();
            this.GlobalContainer = new ObjectContainer();
            this.GlobalContainer.RegisterInstanceAs(this.bindingRegistry);
            this.PluginEvents = new RuntimePluginEvents();
        }

        [TearDown]
        public void TearDownBase()
        {
            this.GlobalContainer.Dispose();
        }

        protected void AssociateRuntimeEventsWithPlugin<TScenarioContainerFinder, TFeatureContainerFinder,
            TTestThreadContainerFinder>(RuntimePluginEvents events)
            where TScenarioContainerFinder : ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>
            where TFeatureContainerFinder : ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>
            where TTestThreadContainerFinder : ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>
        {
            var plugin = new NinjectPlugin();

            this.GlobalContainer.RegisterTypeAs<NinjectTestObjectResolver, ITestObjectResolver>();
            this.GlobalContainer
                .RegisterTypeAs<TScenarioContainerFinder,
                    ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>();
            this.GlobalContainer
                .RegisterTypeAs<TFeatureContainerFinder,
                    ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>>();
            this.GlobalContainer
                .RegisterTypeAs<TTestThreadContainerFinder,
                    ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>>();

            plugin.Initialize(events, Mock.Of<RuntimePluginParameters>(), Mock.Of<UnitTestProviderConfiguration>());
            events.RaiseCustomizeGlobalDependencies(this.GlobalContainer, this.specFlowConfiguration);
        }

        protected IObjectContainer CreateTestThreadContainer(IObjectContainer parentContainer)
        {
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContextManager.cs#L159
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContainerBuilder.cs#L78
            var testThreadContainer = new ObjectContainer(parentContainer);
            this.PluginEvents.RaiseCustomizeTestThreadDependencies(testThreadContainer);

            return testThreadContainer;
        }

        protected IObjectContainer CreateFeatureContainer(IObjectContainer parentContainer)
        {
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContextManager.cs#L168
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContainerBuilder.cs#L112
            var featureContainer = new ObjectContainer(parentContainer);
            featureContainer.RegisterInstanceAs(this.specFlowConfiguration);
            featureContainer.RegisterInstanceAs(
                new FeatureInfo(CultureInfo.CurrentCulture, string.Empty, string.Empty, string.Empty));
            this.PluginEvents.RaiseCustomizeFeatureDependencies(featureContainer);

            return featureContainer;
        }

        protected IObjectContainer CreateScenarioContainer(IObjectContainer parentContainer)
        {
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContextManager.cs#L183
            // cf. https://github.com/SpecFlowOSS/SpecFlow/blob/1f15a7480ef0dbf7a432f01bdccae4fe12fb6539/TechTalk.SpecFlow/Infrastructure/ContainerBuilder.cs#L90
            var scenarioContainer = new ObjectContainer(parentContainer);
            scenarioContainer.RegisterInstanceAs(
                new ScenarioInfo(string.Empty, string.Empty, Array.Empty<string>(), new OrderedDictionary()));
            this.PluginEvents.RaiseCustomizeScenarioDependencies(scenarioContainer);

            return scenarioContainer;
        }

        protected void SetupBindingRegistryWithAssemblyContainingHook(Type type)
        {
            var bindingRegistryMock = Mock.Get(this.bindingRegistry);
            var hookBinding = new Mock<IHookBinding>();
            var methodInfo = type.Methods().First();
            var bindingMethod = new RuntimeBindingMethod(methodInfo);
            hookBinding.SetupGet(binding => binding.Method).Returns(bindingMethod);
            bindingRegistryMock.Setup(registry => registry.GetHooks())
                .Returns(new List<IHookBinding> { hookBinding.Object });
        }

        protected TService ResolveFromCustomContainer<TService>(TContainerType container)
            where TService : class
        {
            return this.resolver(container, typeof(TService)) as TService;
        }

        protected void RegisterTransientInCustomContainer<TService, TInterface>(TContainerType container)
            where TService : class
            where TInterface : class
        {
            this.binder(container, typeof(TService), typeof(TInterface));
        }
    }
}