namespace SpecFlowPluginBase
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using SpecFlowPluginBase.Exceptions;
    using SpecFlowPluginBase.Properties;
    using TechTalk.SpecFlow;

    public static class BindingRegister
    {
        [SuppressMessage(
            "Microsoft.Design",
            "CA1004:Generic methods should provide type parameter",
            Justification = "this is a hack to get a type in the assembly")]
        public static void RegisterBindings<TNonStaticClassInTestAssembly>(Action<Type> bindTypeInSingletonScope)
        {
            var allBindings = typeof(TNonStaticClassInTestAssembly).Assembly.GetTypes()
                .Where(t => Attribute.IsDefined(t, typeof(BindingAttribute)))
                .ToList();

            allBindings.ForEach(bindTypeInSingletonScope);
        }
    }
}