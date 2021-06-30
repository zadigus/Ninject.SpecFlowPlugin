namespace SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using Ninject;
    using NUnit.Framework;
    using SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;

    [TestFixture(typeof(IKernel))]
    public class ChildContainerFixture<TContainerType> : BaseFixture<TContainerType>
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
        public void Scenario_Kernel_Can_Resolve_Object_From_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                {
                    var featureKernel = featureContainer.Resolve<TContainerType>();
                    this.RegisterTransientInCustomContainer<TestClass, ITestClass>(featureKernel);
                    var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                    // Act
                    Action act = () => this.ResolveFromCustomContainer<ITestClass>(scenarioKernel);

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }

        [Test]
        public void Scenario_Kernel_Can_Resolve_Object_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(testThreadContainer))
                {
                    var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                    this.RegisterTransientInCustomContainer<TestClass, ITestClass>(testThreadKernel);
                    var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                    // Act
                    Action act = () => this.ResolveFromCustomContainer<ITestClass>(scenarioKernel);

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }

        [Test]
        public void Feature_Kernel_Can_Resolve_Object_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                    this.RegisterTransientInCustomContainer<TestClass, ITestClass>(testThreadKernel);
                    var featureKernel = featureContainer.Resolve<TContainerType>();

                    // Act
                    Action act = () => this.ResolveFromCustomContainer<ITestClass>(featureKernel);

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }
    }
}