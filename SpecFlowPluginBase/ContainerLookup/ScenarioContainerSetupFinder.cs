namespace SpecFlowPluginBase.ContainerLookup
{
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class
        ScenarioContainerSetupFinder<TContainerType> : ContainerSetupFinder<ScenarioDependenciesAttribute,
            TContainerType>
        where TContainerType : class
    {
        public ScenarioContainerSetupFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}