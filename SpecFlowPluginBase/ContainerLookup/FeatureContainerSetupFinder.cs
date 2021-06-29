namespace SpecFlowPluginBase.ContainerLookup
{
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class FeatureContainerSetupFinder<TContainerType> : ContainerSetupFinder<FeatureDependenciesAttribute, TContainerType>
        where TContainerType : class
    {
        public FeatureContainerSetupFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}