namespace Ninject.SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using Ninject;
    using Ninject.SpecFlowPlugin.Integration.TestObjects;
    using NUnit.Framework;
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow;

    [TestFixture]
    public class ScenarioContextFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<NoOpContainerSetupFinder<ScenarioDependenciesAttribute>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute>>(this.PluginEvents);
        }

        [Test]
        public void Resolved_Scenario_Context_Is_The_Same_For_The_Same_Scenario_Kernel()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                // Act
                var scenarioContext1 = scenarioKernel.Get<ScenarioContext>();
                var scenarioContext2 = scenarioKernel.Get<ScenarioContext>();

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
                var featureKernel = featureContainer.Resolve<IKernel>();

                // Act
                Action act = () => featureKernel.Get<ScenarioContext>();

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
                var testThreadKernel = testThreadContainer.Resolve<IKernel>();

                // Act
                Action act = () => testThreadKernel.Get<ScenarioContext>();

                // Assert
                act.Should().Throw<ActivationException>();
            }
        }
    }
}