namespace SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using Ninject;
    using NUnit.Framework;
    using SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow;

    [TestFixture(typeof(IKernel))]
    public class ScenarioContextFixture<TContainerType> : BaseFixture<TContainerType>
        where TContainerType : class
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<
                NoOpContainerSetupFinder<ScenarioDependenciesAttribute, TContainerType>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>>(this.PluginEvents);
        }

        [Test]
        public void Resolved_Scenario_Context_Is_The_Same_For_The_Same_Scenario_Kernel()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                // Act
                var scenarioContext1 = this.ResolveFromCustomContainer<ScenarioContext>(scenarioKernel);
                var scenarioContext2 = this.ResolveFromCustomContainer<ScenarioContext>(scenarioKernel);

                // Assert
                scenarioContext2.Should().BeSameAs(scenarioContext1);
            }
        }

        [Test]
        public void Scenario_Context_Cannot_Be_Resolved_From_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                var featureKernel = featureContainer.Resolve<TContainerType>();

                // Act
                Action act = () => this.ResolveFromCustomContainer<ScenarioContext>(featureKernel);

                // Assert
                act.Should().Throw<ActivationException>();
            }
        }

        [Test]
        public void Scenario_Context_Cannot_Be_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                var testThreadKernel = testThreadContainer.Resolve<TContainerType>();

                // Act
                Action act = () => this.ResolveFromCustomContainer<ScenarioContext>(testThreadKernel);

                // Assert
                act.Should().Throw<ActivationException>();
            }
        }
    }
}