namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using global::Ninject;
    using NoDependenciesAttribute.Hooks;
    using NUnit.Framework;
    using SpecFlowPluginBase.ContainerLookup;

    [TestFixture]
    public class DependenciesAttributeFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.SetupBindingRegistryWithAssemblyContainingHook(typeof(NoOpHook));
            this.AssociateRuntimeEventsWithPlugin<ScenarioContainerSetupFinder<IKernel>,
                FeatureContainerSetupFinder<IKernel>, TestThreadContainerSetupFinder<IKernel>>(this.PluginEvents);
        }

        [Test]
        public void Does_Not_Throw_When_No_ScenarioDependenciesAttribute_Is_Found_In_Binding_Assemblies()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                // Act
                Action act = () => scenarioContainer.Resolve<IKernel>();

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
                Action act = () => featureContainer.Resolve<IKernel>();

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
                Action act = () => testThreadContainer.Resolve<IKernel>();

                // Assert
                act.Should().NotThrow();
            }
        }
    }
}