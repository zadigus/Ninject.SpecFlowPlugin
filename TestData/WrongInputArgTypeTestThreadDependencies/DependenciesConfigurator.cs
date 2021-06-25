namespace WrongInputArgTypeTestThreadDependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Ninject;
    using Ninject.SpecFlowPlugin.Attributes;

    internal class DependenciesConfigurator
    {
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [TestThreadDependencies]
        public static void SetupTestThreadContainer(StandardKernel myInvalidArg)
        {
            // do nothing
        }
    }
}