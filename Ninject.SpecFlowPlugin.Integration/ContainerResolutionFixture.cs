namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration
{
    using BoDi;
    using FluentAssertions;
    using global::Ninject;
    using NUnit.Framework;
    using Selectron.Testing.Ninject.SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;

    [TestFixture]
    public class ContainerResolutionFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<NoOpContainerSetupFinder<ScenarioDependenciesAttribute>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute>>(this.PluginEvents);
        }

        [Test]
        public void Can_Resolve_Ninject_Kernel_From_ScenarioContainer()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                // Act
                var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                // Assert
                scenarioKernel.Should().NotBeNull();
            }
        }

        [Test]
        public void Can_Resolve_Ninject_Kernel_From_FeatureContainer()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                // Act
                var featureKernel = featureContainer.Resolve<IKernel>();

                // Assert
                featureKernel.Should().NotBeNull();
            }
        }

        [Test]
        public void Can_Resolve_Ninject_Kernel_From_TestThreadContainer()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                // Act
                var testThreadKernel = testThreadContainer.Resolve<IKernel>();

                // Assert
                testThreadKernel.Should().NotBeNull();
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void ObjectContainer_Can_Be_Resolved_From_Scenario_Kernel()
        {
            // Arrange
            using (var expectedScenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                var scenarioKernel = expectedScenarioContainer.Resolve<IKernel>();

                // Act
                var actualScenarioContainer = scenarioKernel.Get<IObjectContainer>();

                // Assert
                actualScenarioContainer.Should().BeSameAs(expectedScenarioContainer);
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void
            ObjectContainer_Resolved_From_Scenario_Kernel_Is_Not_Same_As_ObjectContainer_Resolved_From_Feature_Kernel()
        {
            // Arrange
            using (var featureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                {
                    var featureKernel = featureContainer.Resolve<IKernel>();
                    var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                    // Act
                    var objectContainerFromFeatureKernel = featureKernel.Get<IObjectContainer>();
                    var objectContainerFromScenarioKernel = scenarioKernel.Get<IObjectContainer>();

                    // Assert
                    objectContainerFromScenarioKernel.Should().NotBeSameAs(objectContainerFromFeatureKernel);
                }
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void
            ObjectContainer_Resolved_From_Scenario_Kernel_Is_Not_Same_As_ObjectContainer_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                    {
                        var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                        var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                        // Act
                        var objectContainerFromTestThreadKernel = testThreadKernel.Get<IObjectContainer>();
                        var objectContainerFromScenarioKernel = scenarioKernel.Get<IObjectContainer>();

                        // Assert
                        objectContainerFromScenarioKernel.Should().NotBeSameAs(objectContainerFromTestThreadKernel);
                    }
                }
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void ObjectContainer_Can_Be_Resolved_From_Feature_Kernel()
        {
            // Arrange
            using (var expectedFeatureContainer = this.CreateFeatureContainer(this.GlobalContainer))
            {
                var featureKernel = expectedFeatureContainer.Resolve<IKernel>();

                // Act
                var actualFeatureContainer = featureKernel.Get<IObjectContainer>();

                // Assert
                actualFeatureContainer.Should().BeSameAs(expectedFeatureContainer);
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void
            ObjectContainer_Resolved_From_Feature_Kernel_Is_Not_Same_As_ObjectContainer_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                    var featureKernel = featureContainer.Resolve<IKernel>();

                    // Act
                    var objectContainerFromFeatureKernel = featureKernel.Get<IObjectContainer>();
                    var objectContainerFromTestThreadKernel = testThreadKernel.Get<IObjectContainer>();

                    // Assert
                    objectContainerFromTestThreadKernel.Should().NotBeSameAs(objectContainerFromFeatureKernel);
                }
            }
        }

        [Test]
        [Ignore(
            "bug in ObjectContainer.Dispose that allows to concurrently dispose the same ObjectContainer twice, hence accessing the same reference of ObjectContainer.objectPool but modified during the loop execution")]
        public void ObjectContainer_Can_Be_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var expectedTestThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                var testThreadKernel = expectedTestThreadContainer.Resolve<IKernel>();

                // Act
                var actualTestThreadContainer = testThreadKernel.Get<IObjectContainer>();

                // Assert
                actualTestThreadContainer.Should().BeSameAs(expectedTestThreadContainer);
            }
        }
    }
}