namespace Ninject.SpecFlowPlugin.Test
{
    using System;
    using System.Collections.Generic;
    using BoDi;
    using Moq;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Parameters;
    using Ninject.Planning.Bindings;
    using Ninject.SpecFlowPlugin.Test.TestObjects;
    using NUnit.Framework;

    [TestFixture]
    public class NinjectTestObjectResolverFixture
    {
        private NinjectTestObjectResolver testee;

        [SetUp]
        public void SetUp()
        {
            this.testee = new NinjectTestObjectResolver();
        }

        [Test]
        public void When_ResolveBindingInstance_ItShould_Forward_Resolution_To_Ninject()
        {
            // Arrange
            var objectContainer = new Mock<IObjectContainer>();
            var container = new Mock<IKernel>();
            var resolvedType = typeof(ITestClass);
            var request = Mock.Of<IRequest>();
            container.Setup(
                    c => c.CreateRequest(
                        resolvedType,
                        It.IsAny<Func<IBindingMetadata, bool>>(),
                        It.IsAny<IEnumerable<IParameter>>(),
                        It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .Returns(request);
            container.Setup(c => c.Resolve(request)).Returns(new List<object> { new object() });
            objectContainer.Setup(x => x.Resolve<IKernel>()).Returns(container.Object);

            // Act
            this.testee.ResolveBindingInstance(resolvedType, objectContainer.Object);

            // Assert
            container.Verify(
                x => x.CreateRequest(resolvedType, null, Array.Empty<IParameter>(), false, true),
                Times.Once);
        }
    }
}