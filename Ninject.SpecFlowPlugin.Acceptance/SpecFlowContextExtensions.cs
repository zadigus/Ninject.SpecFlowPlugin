namespace Ninject.SpecFlowPlugin.Acceptance
{
    using System;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    [DebuggerStepThrough]
    public static class SpecFlowContextExtensions
    {
        public static void Save<T>(this SpecFlowContext specFlowContext, T value, Enum key)
        {
            specFlowContext.Set(value, key.ToString());
        }

        public static T Get<T>(this SpecFlowContext specFlowContext, Enum key)
        {
            if (!specFlowContext.ContainsKey(key))
            {
                return default;
            }

            return specFlowContext.Get<T>(key.ToString());
        }

        public static bool ContainsKey(this SpecFlowContext specFlowContext, Enum key)
        {
            return specFlowContext.ContainsKey(key.ToString());
        }
    }
}