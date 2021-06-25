namespace SpecFlowPluginBase.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TechTalk.SpecFlow.Bindings;
    using TechTalk.SpecFlow.Bindings.Reflection;

    public static class BindingRegistryExtensions
    {
        public static IEnumerable<IBindingType> GetBindingTypes(this IBindingRegistry bindingRegistry)
        {
            bindingRegistry.CheckNullArgument(nameof(bindingRegistry));

            return bindingRegistry.GetStepDefinitions()
                .Concat<IBinding>(bindingRegistry.GetHooks())
                .Concat(bindingRegistry.GetStepTransformations())
                .Select(b => b.Method.Type)
                .Distinct();
        }

        public static IEnumerable<Assembly> GetBindingAssemblies(this IBindingRegistry bindingRegistry)
        {
            return bindingRegistry.GetBindingTypes()
                .OfType<RuntimeBindingType>()
                .Select(t => t.Type.Assembly)
                .Distinct();
        }
    }
}