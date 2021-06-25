namespace WrongInputArgTypeScenarioDependencies
{
    using System.Diagnostics.CodeAnalysis;
    using Ninject.SpecFlowPlugin.Attributes;

    internal class DependenciesConfigurator
    {
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [ScenarioDependencies]
        public static void SetupScenarioContainer(string myInvalidArg)
        {
            // do nothing
        }
    }
}