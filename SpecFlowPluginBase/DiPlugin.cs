﻿namespace SpecFlowPluginBase
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using BoDi;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.ContainerLookup;
    using TechTalk.SpecFlow.Infrastructure;
    using TechTalk.SpecFlow.Plugins;
    using TechTalk.SpecFlow.UnitTestProvider;

    public abstract class DiPlugin<TContainerType, TTestObjectResolver> : IRuntimePlugin
        where TContainerType : class
        where TTestObjectResolver : TestObjectResolver<TContainerType>
    {
        private readonly object registrationLock = new object();

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
                if (!args.ObjectContainer
                    .IsRegistered<ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>())
                {
                    // an extra lock to ensure that there are not two super fast threads re-registering the same stuff
                    lock (this.registrationLock)
                    {
                        if (!args.ObjectContainer
                            .IsRegistered<ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>())
                        {
                            args.ObjectContainer
                                .RegisterTypeAs<TTestObjectResolver, ITestObjectResolver>();
                            args.ObjectContainer
                                .RegisterTypeAs<TestThreadContainerSetupFinder<TContainerType>,
                                    ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>>();
                            args.ObjectContainer
                                .RegisterTypeAs<FeatureContainerSetupFinder<TContainerType>,
                                    ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>>();
                            args.ObjectContainer
                                .RegisterTypeAs<ScenarioContainerSetupFinder<TContainerType>,
                                    ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>();
                        }
                    }

                    // workaround for parallel execution issue - this should be rather a feature in BoDi?
                    args.ObjectContainer.Resolve<ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>>();
                    args.ObjectContainer.Resolve<ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>>();
                    args.ObjectContainer.Resolve<ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>();
                }
            };

            runtimePluginEvents.CustomizeTestThreadDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterFactoryAs(
                    () =>
                    {
                        var objectContainer = args.ObjectContainer;
                        var containerFinder = objectContainer
                            .Resolve<ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = this.CreateTestThreadContainer(objectContainer);
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
                        var containerFinder = objectContainer
                            .Resolve<ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = this.CreateFeatureContainer(objectContainer);
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
                        var containerFinder = objectContainer
                            .Resolve<ContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>>();
                        var setupContainer = containerFinder.SetupContainerFunc();
                        var container = this.CreateScenarioContainer(objectContainer);
                        setupContainer?.Invoke(container);

                        return container;
                    });
            };
        }

        protected abstract TContainerType CreateScenarioContainer(ObjectContainer objectContainer);

        protected abstract TContainerType CreateFeatureContainer(ObjectContainer objectContainer);

        protected abstract TContainerType CreateTestThreadContainer(ObjectContainer objectContainer);
    }
}