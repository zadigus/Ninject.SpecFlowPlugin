namespace SpecFlowPluginBase.ContainerLookup
{
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class TestThreadContainerFinder<TContainerType> : ContainerFinder<TestThreadDependenciesAttribute, TContainerType>
        where TContainerType : class
    {
        public TestThreadContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}