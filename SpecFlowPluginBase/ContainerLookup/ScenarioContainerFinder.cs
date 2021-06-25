namespace SpecFlowPluginBase.ContainerLookup
{
    using System;
    using System.Globalization;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.Exceptions;
    using SpecFlowPluginBase.Properties;
    using TechTalk.SpecFlow.Bindings;

    public class ScenarioContainerFinder<TContainerType> : ContainerFinder<ScenarioDependenciesAttribute, TContainerType>
        where TContainerType : class
    {
        public ScenarioContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }

        protected override Action<TContainerType> FindSetupContainer()
        {
            var setupContainerAction = base.FindSetupContainer();

            return setupContainerAction ?? throw new SpecFlowPluginException(
                string.Format(CultureInfo.CurrentCulture, Resources.ScenarioDependenciesNotFound),
                SpecFlowPluginError.ScenarioDependenciesNotFound);
        }
    }
}