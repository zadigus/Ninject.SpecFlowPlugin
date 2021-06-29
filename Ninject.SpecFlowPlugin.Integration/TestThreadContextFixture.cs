namespace Selectron.Testing.Ninject.SpecFlowPlugin.Integration
{
    using System;
    using FluentAssertions;
    using global::Ninject;
    using NUnit.Framework;
    using Selectron.Testing.Ninject.SpecFlowPlugin.Integration.TestObjects;
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow;

    [TestFixture]
    public class TestThreadContextFixture : BaseFixture
    {
        [SetUp]
        public void SetUp()
        {
            this.AssociateRuntimeEventsWithPlugin<NoOpContainerSetupFinder<ScenarioDependenciesAttribute>,
                NoOpContainerSetupFinder<FeatureDependenciesAttribute>,
                NoOpContainerSetupFinder<TestThreadDependenciesAttribute>>(this.PluginEvents);
        }

        [Test]
        public void Resolved_TestThread_Context_Is_The_Same_For_The_Same_TestThread_Kernel()
        {
            // Arrange
            using (var testThreadContainer = this.CreateTestThreadContainer(this.GlobalContainer))
            {
                // Act
                var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                var testThreadContext1 = testThreadKernel.Get<TestThreadContext>();
                var testThreadContext2 = testThreadKernel.Get<TestThreadContext>();

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
                        var scenarioKernel = scenarioContainer.Resolve<IKernel>();

                        // Act
                        Action act = () => scenarioKernel.Get<TestThreadContext>();

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
                    var featureKernel = featureContainer.Resolve<IKernel>();

                    // Act
                    Action act = () => featureKernel.Get<TestThreadContext>();

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
                var testThreadKernel = testThreadContainer.Resolve<IKernel>();

                // Act
                Action act = () => testThreadKernel.Get<TestThreadContext>();

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
                        var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                        var featureKernel = featureContainer.Resolve<IKernel>();
                        var scenarioKernel = scenarioContainer.Resolve<IKernel>();
                        var testThreadContextFromTestThreadKernel = testThreadKernel.Get<TestThreadContext>();
                        var testThreadContextFromFeatureKernel = featureKernel.Get<TestThreadContext>();
                        var testThreadContextFromScenarioKernel = scenarioKernel.Get<TestThreadContext>();

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
                            var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                            var testThreadContextFromTestThreadKernel = testThreadKernel.Get<TestThreadContext>();
                            var scenarioKernel1 = scenarioContainer1.Resolve<IKernel>();
                            var testThreadContextFromScenarioKernel1 = scenarioKernel1.Get<TestThreadContext>();
                            var scenarioKernel2 = scenarioContainer2.Resolve<IKernel>();
                            var testThreadContextFromScenarioKernel2 = scenarioKernel2.Get<TestThreadContext>();

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
                        var testThreadKernel = testThreadContainer.Resolve<IKernel>();
                        var testThreadContextFromTestThreadKernel = testThreadKernel.Get<TestThreadContext>();
                        var featureKernel1 = featureContainer1.Resolve<IKernel>();
                        var testThreadContextFromFeatureKernel1 = featureKernel1.Get<TestThreadContext>();
                        var featureKernel2 = featureContainer2.Resolve<IKernel>();
                        var testThreadContextFromFeatureKernel2 = featureKernel2.Get<TestThreadContext>();

                        // Assert
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromFeatureKernel1);
                        testThreadContextFromTestThreadKernel.Should().BeSameAs(testThreadContextFromFeatureKernel2);
                    }
                }
            }
        }
    }
}