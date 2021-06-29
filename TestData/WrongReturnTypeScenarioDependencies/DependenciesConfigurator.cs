namespace WrongReturnTypeScenarioDependencies
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
        [ScenarioDependencies]
        public static string SetupScenarioContainer(IKernel kernel)
        {
            // do nothing
            return string.Empty;
        }
    }
}