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
    public class FeatureContextFixture<TContainerType> : BaseFixture<TContainerType>
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
        public void Resolved_Feature_Context_Is_The_Same_For_The_Same_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                // Act
                var featureKernel = featureContainer.Resolve<TContainerType>();
                var featureContext1 = this.ResolveFromCustomContainer<FeatureContext>(featureKernel);
                var featureContext2 = this.ResolveFromCustomContainer<FeatureContext>(featureKernel);

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
                    var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                    // Act
                    Action act = () => this.ResolveFromCustomContainer<FeatureContext>(scenarioKernel);

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
                var featureKernel = featureContainer.Resolve<TContainerType>();

                // Act
                Action act = () => this.ResolveFromCustomContainer<FeatureContext>(featureKernel);

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
                    var featureKernel = featureContainer.Resolve<TContainerType>();
                    var scenarioKernel = scenarioContainer.Resolve<TContainerType>();
                    var featureContextFromFeatureKernel = this.ResolveFromCustomContainer<FeatureContext>(featureKernel);
                    var featureContextFromScenarioKernel = this.ResolveFromCustomContainer<FeatureContext>(scenarioKernel);

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
                var testThreadKernel = testThreadContainer.Resolve<TContainerType>();

                // Act
                Action act = () => this.ResolveFromCustomContainer<FeatureContext>(testThreadKernel);

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
                        var featureKernel = featureContainer.Resolve<TContainerType>();
                        var featureContextFromFeatureKernel = this.ResolveFromCustomContainer<FeatureContext>(featureKernel);
                        var scenarioKernel1 = scenarioContainer1.Resolve<TContainerType>();
                        var featureContextFromScenarioKernel1 = this.ResolveFromCustomContainer<FeatureContext>(scenarioKernel1);
                        var scenarioKernel2 = scenarioContainer2.Resolve<TContainerType>();
                        var featureContextFromScenarioKernel2 = this.ResolveFromCustomContainer<FeatureContext>(scenarioKernel2);

                        // Assert
                        featureContextFromFeatureKernel.Should().BeSameAs(featureContextFromScenarioKernel1);
                        featureContextFromScenarioKernel1.Should().BeSameAs(featureContextFromScenarioKernel2);
                    }
                }
            }
        }
    }
}