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
    public class FeatureContextFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<NoOpContainerSetupFinder<ScenarioDependenciesAttribute>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute>>(this.PluginEvents);
        }

        [Test]
        public void Resolved_Feature_Context_Is_The_Same_For_The_Same_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                // Act
                var featureKernel = featureContainer.Resolve<IKernel>();
                var featureContext1 = featureKernel.Get<FeatureContext>();
                var featureContext2 = featureKernel.Get<FeatureContext>();

                // Assert
                featureContext2.Should().BeSameAs(featureContext1);
            }
        }

        [Test]
        public void Feature_Context_Can_Be_Resolved_From_Scenario_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                {
                    var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                    // Act
                    Action act = () => scenarioKernel.Get<FeatureContext>();

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }

        [Test]
        public void Feature_Context_Can_Be_Resolved_From_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                var featureKernel = featureContainer.Resolve<IKernel>();

                // Act
                Action act = () => featureKernel.Get<FeatureContext>();

                // Assert
                act.Should().NotThrow();
            }
        }

        [Test]
        public void Resolved_Feature_Context_Is_The_Same_In_Both_Scenario_Kernel_And_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                {
                    // Act
                    var featureKernel = featureContainer.Resolve<IKernel>();
                    var scenarioKernel = scenarioContainer.Resolve<IKernel>();
                    var featureContextFromFeatureKernel = featureKernel.Get<FeatureContext>();
                    var featureContextFromScenarioKernel = scenarioKernel.Get<FeatureContext>();

                    // Assert
                    featureContextFromFeatureKernel.Should().BeSameAs(featureContextFromScenarioKernel);
                }
            }
        }

        [Test]
        public void Feature_Context_Cannot_Be_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                var testThreadKernel = testThreadContainer.Resolve<IKernel>();

                // Act
                Action act = () => testThreadKernel.Get<FeatureContext>();

                // Assert
                act.Should().Throw<ActivationException>();
            }
        }

        [Test]
        public void Feature_Contexts_Persist_Across_Scenarios()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer1 = this.CreateScenarioContainer(featureContainer))
                {
                    using (var scenarioContainer2 = this.CreateScenarioContainer(featureContainer))
                    {
                        // Act
                        var featureKernel = featureContainer.Resolve<IKernel>();
                        var featureContextFromFeatureKernel = featureKernel.Get<FeatureContext>();
                        var scenarioKernel1 = scenarioContainer1.Resolve<IKernel>();
                        var featureContextFromScenarioKernel1 = scenarioKernel1.Get<FeatureContext>();
                        var scenarioKernel2 = scenarioContainer2.Resolve<IKernel>();
                        var featureContextFromScenarioKernel2 = scenarioKernel2.Get<FeatureContext>();

                        // Assert
                        featureContextFromFeatureKernel.Should().BeSameAs(featureContextFromScenarioKernel1);
                        featureContextFromScenarioKernel1.Should().BeSameAs(featureContextFromScenarioKernel2);
                    }
                }
            }
        }
    }
}