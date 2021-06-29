namespace SpecFlowPluginBase.ContainerLookup
{
    using SpecFlowPluginBase.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class TestThreadContainerSetupFinder<TContainerType> : ContainerSetupFinder<TestThreadDependenciesAttribute, TContainerType>
        where TContainerType : class
    {
        public TestThreadContainerSetupFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}