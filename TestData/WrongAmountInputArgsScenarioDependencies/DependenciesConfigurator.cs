namespace WrongAmountInputArgsScenarioDependencies
{
    using System.Diagnostics.CodeAnalysis;
    using SpecFlowPluginBase.Attributes;

    internal class DependenciesConfigurator
    {
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1811:AvoidUncalledPrivateCode",
            Justification = "the call is indirect")]
        [ScenarioDependencies]
        public static void SetupScenarioContainer()
        {
            // do nothing
        }
    }
}