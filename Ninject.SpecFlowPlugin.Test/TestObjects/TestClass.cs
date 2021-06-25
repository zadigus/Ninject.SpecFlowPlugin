namespace Ninject.SpecFlowPlugin.Test.TestObjects
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "instantiation is indirect")]
    internal class TestClass : ITestClass
    {
    }
}