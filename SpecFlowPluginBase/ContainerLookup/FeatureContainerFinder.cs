namespace SpecFlowPluginBase.ContainerLookup
{
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class FeatureContainerFinder<TContainerType> : ContainerFinder<FeatureDependenciesAttribute, TContainerType>
        where TContainerType : class
    {
        public FeatureContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}