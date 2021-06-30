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
    public class TestThreadContextFixture<TContainerType> : BaseFixture<TContainerType>
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
        public void Resolved_TestThread_Context_Is_The_Same_For_The_Same_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                // Act
                var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                var testThreadContext1 = this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);
                var testThreadContext2 = this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);

                // Assert
                testThreadContext2.Should().BeSameAs(testThreadContext1);
            }
        }

        [Test]
        public void TestThread_Context_Can_Be_Resolved_From_Scenario_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                    {
                        var scenarioKernel = scenarioContainer.Resolve<TContainerType>();

                        // Act
                        Action act = () => this.ResolveFromCustomContainer<TestThreadContext>(scenarioKernel);

                        // Assert
                        act.Should().NotThrow();
                    }
                }
            }
        }

        [Test]
        public void TestThread_Context_Can_Be_Resolved_From_Feature_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    var featureKernel = featureContainer.Resolve<TContainerType>();

                    // Act
                    Action act = () => this.ResolveFromCustomContainer<TestThreadContext>(featureKernel);

                    // Assert
                    act.Should().NotThrow();
                }
            }
        }

        [Test]
        public void TestThread_Context_Can_Be_Resolved_From_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                var testThreadKernel = testThreadContainer.Resolve<TContainerType>();

                // Act
                Action act = () => this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);

                // Assert
                act.Should().NotThrow();
            }
        }

        [Test]
        public void
            Resolved_TestThread_Context_Is_The_Same_In_Scenario_Kernel_And_Feature_Kernel_And_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    using (var scenarioContainer = this.CreateScenarioContainer(featureContainer))
                    {
                        // Act
                        var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                        var featureKernel = featureContainer.Resolve<TContainerType>();
                        var scenarioKernel = scenarioContainer.Resolve<TContainerType>();
                        var testThreadContextFromTestThreadKernel = this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);
                        var testThreadContextFromFeatureKernel = this.ResolveFromCustomContainer<TestThreadContext>(featureKernel);
                        var testThreadContextFromScenarioKernel = this.ResolveFromCustomContainer<TestThreadContext>(scenarioKernel);

                        // Assert
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromScenarioKernel);
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromFeatureKernel);
                    }
                }
            }
        }

        [Test]
        public void TestThread_Contexts_Persist_Across_Scenarios()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer = this.CreateFeatureContainer(testThreadContainer))
                {
                    using (var scenarioContainer1 = this.CreateScenarioContainer(featureContainer))
                    {
                        using (var scenarioContainer2 = this.CreateScenarioContainer(featureContainer))
                        {
                            // Act
                            var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                            var testThreadContextFromTestThreadKernel = this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);
                            var scenarioKernel1 = scenarioContainer1.Resolve<TContainerType>();
                            var testThreadContextFromScenarioKernel1 = this.ResolveFromCustomContainer<TestThreadContext>(scenarioKernel1);
                            var scenarioKernel2 = scenarioContainer2.Resolve<TContainerType>();
                            var testThreadContextFromScenarioKernel2 = this.ResolveFromCustomContainer<TestThreadContext>(scenarioKernel2);

                            // Assert
                            testThreadContextFromTestThreadKernel.Should()
                                .BeSameAs(testThreadContextFromScenarioKernel1);
                            testThreadContextFromTestThreadKernel.Should()
                                .BeSameAs(testThreadContextFromScenarioKernel2);
                        }
                    }
                }
            }
        }

        [Test]
        public void TestThread_Contexts_Persist_Across_Features()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                using (var featureContainer1 = this.CreateFeatureContainer(testThreadContainer))
                {
                    using (var featureContainer2 = this.CreateFeatureContainer(testThreadContainer))
                    {
                        // Act
                        var testThreadKernel = testThreadContainer.Resolve<TContainerType>();
                        var testThreadContextFromTestThreadKernel = this.ResolveFromCustomContainer<TestThreadContext>(testThreadKernel);
                        var featureKernel1 = featureContainer1.Resolve<TContainerType>();
                        var testThreadContextFromFeatureKernel1 = this.ResolveFromCustomContainer<TestThreadContext>(featureKernel1);
                        var featureKernel2 = featureContainer2.Resolve<TContainerType>();
                        var testThreadContextFromFeatureKernel2 = this.ResolveFromCustomContainer<TestThreadContext>(featureKernel2);

                        // Assert
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromFeatureKernel1);
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromFeatureKernel2);
                    }
                }
            }
        }
    }
}