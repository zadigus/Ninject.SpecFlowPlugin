namespace Ninject.SpecFlowPlugin.ContainerLookup
{
    using Ninject.SpecFlowPlugin.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class FeatureContainerFinder : ContainerFinder<FeatureDependenciesAttribute>
    {
        public FeatureContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}