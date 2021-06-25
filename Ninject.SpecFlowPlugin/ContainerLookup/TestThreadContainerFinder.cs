namespace Ninject.SpecFlowPlugin.ContainerLookup
{
    using Ninject.SpecFlowPlugin.Attributes;
    using TechTalk.SpecFlow.Bindings;

    public class TestThreadContainerFinder : ContainerFinder<TestThreadDependenciesAttribute>
    {
        public TestThreadContainerFinder(IBindingRegistry bindingRegistry)
            : base(bindingRegistry)
        {
        }
    }
}