using Ninject.SpecFlowPlugin;
using TechTalk.SpecFlow.Plugins;

[assembly: RuntimePlugin(typeof(NinjectPlugin))]

namespace Ninject.SpecFlowPlugin
{
    using System;
    using BoDi;
    using Ninject.SpecFlowPlugin.TestContainers;
    using SpecFlowPluginBase;
    using TechTalk.SpecFlow;

    public class NinjectPlugin : DiPlugin<IKernel, NinjectTestObjectResolver>
    {
        protected override IKernel CreateScenarioContainer(ObjectContainer objectContainer)
        {
            if (objectContainer == null)
            {
                throw new ArgumentNullException(nameof(objectContainer));
            }

            var kernel = CreateAcceptanceTestKernel(objectContainer);
            kernel.Bind<IObjectContainer>().ToConstant(objectContainer);
            kernel.Bind<ScenarioContext>().ToMethod(ctx => objectContainer.Resolve<ScenarioContext>());

            return kernel;
        }

        protected override IKernel CreateFeatureContainer(ObjectContainer objectContainer)
        {
            if (objectContainer == null)
            {
                throw new ArgumentNullException(nameof(objectContainer));
            }

            var kernel = CreateAcceptanceTestKernel(objectContainer);
            kernel.Bind<IObjectContainer>().ToConstant(objectContainer);
            kernel.Bind<FeatureContext>().ToMethod(ctx => objectContainer.Resolve<FeatureContext>());

            return kernel;
        }

        protected override IKernel CreateTestThreadContainer(ObjectContainer objectContainer)
        {
            if (objectContainer == null)
            {
                throw new ArgumentNullException(nameof(objectContainer));
            }

            var kernel = CreateAcceptanceTestKernel(objectContainer);
            kernel.Bind<IObjectContainer>().ToConstant(objectContainer);
            kernel.Bind<TestThreadContext>().ToMethod(ctx => objectContainer.Resolve<TestThreadContext>());

            return kernel;
        }

        private static IKernel CreateAcceptanceTestKernel(ObjectContainer objectContainer)
        {
            var baseObjectContainer = objectContainer.BaseContainer;

            if (baseObjectContainer.IsRegistered<IKernel>())
            {
                var baseKernel = baseObjectContainer.Resolve<IKernel>();
                return new ChildAcceptanceTestKernel(baseKernel);
            }

            return new AcceptanceTestKernel();
        }
    }
}