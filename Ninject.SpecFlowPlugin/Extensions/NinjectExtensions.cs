namespace Ninject.SpecFlowPlugin.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Ninject.SpecFlowPlugin.Exceptions;
    using Ninject.SpecFlowPlugin.Properties;
    using TechTalk.SpecFlow;

    public static class NinjectExtensions
    {
        [SuppressMessage(
            "Microsoft.Design",
            "CA1004:Generic methods should provide type parameter",
            Justification = "this is a hack to get a type in the assembly")]
        public static void RegisterBindings<TNonStaticClassInTestAssembly>(this IKernel kernel)
        {
            var allBindings = typeof(TNonStaticClassInTestAssembly).Assembly.GetTypes()
                .Where(t => Attribute.IsDefined(t, typeof(BindingAttribute)))
                .ToList();

            allBindings.ForEach(type => kernel.Bind(type).ToSelf().InSingletonScope());

            var invalidBindings = allBindings.Where(BindingContainsAfterHookWithTooHighOrder).ToList();

            if (invalidBindings.Count > 0)
            {
                var hookNames = invalidBindings.Select(hook => hook.Name);
                var listOfHooks = string.Join(",", hookNames);
                throw new NinjectPluginException(
                    string.Format(CultureInfo.CurrentCulture, Resources.TooHighOrderInAfterHook, listOfHooks),
                    NinjectPluginError.IncompatibleHookFound);
            }
        }

        private static bool BindingContainsAfterHookWithTooHighOrder(Type binding)
        {
            var methods = binding.GetMethods();
            return methods.Any(
                method =>
                {
                    var afterScenarioAttributes = method.GetCustomAttributes(typeof(AfterScenarioAttribute), false)
                        .Cast<HookAttribute>();
                    var afterFeatureAttributes = method.GetCustomAttributes(typeof(AfterFeatureAttribute), false)
                        .Cast<HookAttribute>();
                    var afterTestRunAttributes = method.GetCustomAttributes(typeof(AfterTestRunAttribute), false)
                        .Cast<HookAttribute>();
                    var allAttributes = afterScenarioAttributes.Concat(afterFeatureAttributes)
                        .Concat(afterTestRunAttributes);
                    return allAttributes.Any(attribute => attribute.Order >= Constants.KernelDisposerOrder);
                });
        }
    }
}