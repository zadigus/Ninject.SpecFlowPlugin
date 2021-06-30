namespace SpecFlowPlugin.Integration
{
    using BoDi;
    using FluentAssertions;
    using Ninject;
    using NUnit.Framework;
    using SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;

    [TestFixture(typeof(IKernel))]
    public class ContainerResolutionFixture<TContainerType> : BaseFixture<TContainerType>
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
        public void Can_Resolve_Ninject_Kernel_From_ScenarioContainer()
        {
            // Arrange
            using (var scenarioContainer = this.CreateScenarioContainer(this.GlobalContainer))
            {
                // Act
                var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

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
                var featureKernel = featureContainer.Resolve<TContainerType>();

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
                var testThreadKernel = testThreadContainer.Resolve<TContainerType>();

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
                var scenarioKernel = expectedScenarioContainer.Resolve<TContainerType>();

                // Act
                var actualScenarioContainer = this.ResolveFromCustomContainer<IObjectContainer>(scenarioKernel);

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
                    var featureKernel = featureContainer.Resolve<TContainerType>();
                    var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                    // Act
                    var objectContainerFromFeatureKernel = this.ResolveFromCustomContainer<IObjectContainer>(featureKernel);
                    var objectContainerFromScenarioKernel = this.ResolveFromCustomContainer<IObjectContainer>(scenarioKernel);

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
                        var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                        var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                        // Act
                        var objectContainerFromTestThreadKernel = this.ResolveFromCustomContainer<IObjectContainer>(testThreadKernel);
                        var objectContainerFromScenarioKernel = this.ResolveFromCustomContainer<IObjectContainer>(scenarioKernel);

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
                var featureKernel = expectedFeatureContainer.Resolve<TContainerType>();

                // Act
                var actualFeatureContainer = this.ResolveFromCustomContainer<IObjectContainer>(featureKernel);

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
                    var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                    var featureKernel = featureContainer.Resolve<TContainerType>();

                    // Act
                    var objectContainerFromFeatureKernel = this.ResolveFromCustomContainer<IObjectContainer>(featureKernel);
                    var objectContainerFromTestThreadKernel = this.ResolveFromCustomContainer<IObjectContainer>(testThreadKernel);

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
                var testThreadKernel = expectedTestThreadContainer.Resolve<TContainerType>();

                // Act
                var actualTestThreadContainer = this.ResolveFromCustomContainer<IObjectContainer>(testThreadKernel);

                // Assert
                actualTestThreadContainer.Should().BeSameAs(expectedTestThreadContainer);
            }
        }
    }
}