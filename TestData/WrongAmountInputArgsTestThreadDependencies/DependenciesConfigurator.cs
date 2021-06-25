namespace WrongAmountInputArgsTestThreadDependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Ninject;
    using SpecFlowPluginBase.Attributes;

    internal class DependenciesConfigurator
    {
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [TestThreadDependencies]
        public static void SetupTestThreadContainer(IKernel kernel, string invalidArg)
        {
            // do nothing
        }
    }
}