namespace WrongReturnTypeTestThreadDependencies
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
        public static IKernel SetupTestThreadContainer(IKernel kernel)
        {
            // do nothing
            return null;
        }
    }
}