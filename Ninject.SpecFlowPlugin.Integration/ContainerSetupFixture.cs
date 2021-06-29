namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration
{
    using System;
    using System.Reflection;
    using FluentAssertions;
    using global::Ninject;
    using NUnit.Framework;
    using SpecFlowPluginBase.ContainerLookup;
    using SpecFlowPluginBase.Exceptions;
    using WrongReturnTypeScenarioDependencies.Hooks;

    [TestFixture]
    public class ContainerSetupFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<ScenarioContainerSetupFinder<IKernel>,
                FeatureContainerSetupFinder<IKernel>, TestThreadContainerSetupFinder<IKernel>>(this.PluginEvents);
        }

        [TestCase(
            typeof(NoOpHook),
            Description = "The DependenciesConfigurator.SetupScenarioContainer method does not return void.")]
        [TestCase(
            typeof(WrongInputArgTypeScenarioDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupScenarioContainer method's argument is not of type IKernel.")]
        [TestCase(
            typeof(WrongAmountInputArgsScenarioDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupScenarioContainer method does not take one single input argument.")]
        public void Throws_When_Wrong_Scenario_Container_Setup_Method_Signature(Type bindingClassType)
        {
            // Arrange
            this.SetupBindingRegistryWithAssemblyContainingHook(bindingClassType);
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => scenarioContainer.Resolve<IKernel>();

                // Assert
                act.Should()
                    .Throw<TargetInvocationException>()
                    .WithInnerException<WrongContainerSetupSignatureException>();
            }
        }

        [TestCase(
            typeof(WrongReturnTypeFeatureDependencies.Hooks.NoOpHook),
            Description = "The DependenciesConfigurator.SetupFeatureContainer method does not return void.")]
        [TestCase(
            typeof(WrongInputArgTypeFeatureDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupFeatureContainer method's argument is not of type IKernel.")]
        [TestCase(
            typeof(WrongAmountInputArgsFeatureDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupFeatureContainer method does not take one single input argument.")]
        public void Throws_When_Wrong_Feature_Container_Setup_Method_Signature(Type bindingClassType)
        {
            // Arrange
            this.SetupBindingRegistryWithAssemblyContainingHook(bindingClassType);
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => featureContainer.Resolve<IKernel>();

                // Assert
                act.Should()
                    .Throw<TargetInvocationException>()
                    .WithInnerException<WrongContainerSetupSignatureException>();
            }
        }

        [TestCase(
            typeof(WrongReturnTypeTestThreadDependencies.Hooks.NoOpHook),
            Description = "The DependenciesConfigurator.SetupTestThreadContainer method does not return void.")]
        [TestCase(
            typeof(WrongInputArgTypeTestThreadDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupTestThreadContainer method's argument is not of type IKernel.")]
        [TestCase(
            typeof(WrongAmountInputArgsTestThreadDependencies.Hooks.NoOpHook),
            Description =
                "The DependenciesConfigurator.SetupTestThreadContainer method does not take one single input argument.")]
        public void Throws_When_Wrong_TestThread_Container_Setup_Method_Signature(Type bindingClassType)
        {
            // Arrange
            this.SetupBindingRegistryWithAssemblyContainingHook(bindingClassType);
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => testThreadContainer.Resolve<IKernel>();

                // Assert
                act.Should()
                    .Throw<TargetInvocationException>()
                    .WithInnerException<WrongContainerSetupSignatureException>();
            }
        }
    }
}