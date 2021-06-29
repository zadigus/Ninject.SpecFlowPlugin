namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using global::Ninject;
    using NUnit.Framework;
    using Selectron.Testing.Ninject.SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;

    [TestFixture]
    public class ChildContainerFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<NoOpContainerSetupFinder<ScenarioDependenciesAttribute>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute>>(this.PluginEvents);
        }

        [Test]
        public void Scenario_Kernel_Can_Resolve_Object_From_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                {
                    var featureKernel = featureContainer.Resolve<IKernel>();
                    featureKernel.Bind<ITestClass>().To<TestClass>();
                    var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                    // Act
                    Action act = () => scenarioKernel.Get<ITestClass>();

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
                    var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                    testThreadKernel.Bind<ITestClass>().To<TestClass>();
                    var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                    // Act
                    Action act = () => scenarioKernel.Get<ITestClass>();

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
                    var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                    testThreadKernel.Bind<ITestClass>().To<TestClass>();
                    var featureKernel = featureContainer.Resolve<IKernel>();

                    // Act
                    Action act = () => featureKernel.Get<ITestClass>();

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }
    }
}