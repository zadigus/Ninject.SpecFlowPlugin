namespace SpecFlowPluginBase
{
    using System;
    using System.Diagnostics;

    [DebuggerStepThrough]
    public static class ArgumentChecker
    {
        public static void CheckNullArgument(this object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}