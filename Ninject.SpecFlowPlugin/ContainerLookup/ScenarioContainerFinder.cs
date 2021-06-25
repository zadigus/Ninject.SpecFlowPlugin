namespace Ninject.SpecFlowPlugin.ContainerLookup
{
    using System;
    using System.Globalization;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.Exceptions;
    using Ninject.SpecFlowPlugin.Properties;
    using TechTalk.SpecFlow.Bindings;

    public class ScenarioContainerFinder : ContainerFinder<ScenarioDependenciesAttribute>
    {
        public ScenarioContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }

        protected override Action<IKernel> FindSetupContainer()
        {
            var setupContainerAction = base.FindSetupContainer();

            return setupContainerAction ?? throw new NinjectPluginException(
                string.Format(CultureInfo.CurrentCulture, Resources.ScenarioDependenciesNotFound),
                NinjectPluginError.ScenarioDependenciesNotFound);
        }
    }
}