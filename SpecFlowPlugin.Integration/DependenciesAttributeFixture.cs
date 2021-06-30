namespace SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using Ninject;
    using NoDependenciesAttribute.Hooks;
    using NUnit.Framework;
    using SpecFlowPluginBase.ContainerLookup;

    [TestFixture(typeof(IKernel))]
    public class DependenciesAttributeFixture<TContainerType> : BaseFixture<TContainerType>
        where TContainerType : class
    {
        [SetUp]
        public void SetUp()
        {
            this.SetupBindingRegistryWithAssemblyContainingHook(typeof(NoOpHook));
            this.AssociateRuntimeEventsWithPlugin<ScenarioContainerSetupFinder<TContainerType>,
                FeatureContainerSetupFinder<TContainerType>, TestThreadContainerSetupFinder<TContainerType>>(
                this.PluginEvents);
        }

        [Test]
        public void Does_Not_Throw_When_No_ScenarioDependenciesAttribute_Is_Found_In_Binding_Assemblies()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => scenarioContainer.Resolve<TContainerType>();

                // Assert
                act.Should().NotThrow();
            }
        }

        [Test]
        public void Does_Not_Throw_When_No_FeatureDependenciesAttribute_Is_Found_In_Binding_Assemblies()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => featureContainer.Resolve<TContainerType>();

                // Assert
                act.Should().NotThrow();
            }
        }

        [Test]
        public void Does_Not_Throw_When_No_TestThreadDependenciesAttribute_Is_Found_In_Binding_Assemblies()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => testThreadContainer.Resolve<TContainerType>();

                // Assert
                act.Should().NotThrow();
            }
        }
    }
}