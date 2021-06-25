namespace Ninject.SpecFlowPlugin.ContainerLookup
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Ninject.SpecFlowPlugin.Attributes;
    using Ninject.SpecFlowPlugin.Exceptions;
    using Ninject.SpecFlowPlugin.Properties;

    internal static class ContainerConfiguratorChecker<TConfiguratorType>
        where TConfiguratorType : ContainerConfigurationAttribute
    {
        public static void CheckMethodSignature(MethodInfo info)
        {
            CheckExpectedReturnType(info);
            CheckSingleArgumentIsExpected(info);
            CheckExpectedArgumentType<IKernel>(info);
        }

        private static void CheckExpectedReturnType(MethodInfo info)
        {
            var actualReturnType = info.ReturnType;
            if (actualReturnType != typeof(void))
            {
                throw new NinjectPluginException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongContainerConfiguratorReturnType,
                        typeof(TConfiguratorType),
                        actualReturnType),
                    NinjectPluginError.WrongDependenciesSetupMethodSignature);
            }
        }

        private static void CheckSingleArgumentIsExpected(MethodInfo info)
        {
            const int expectedAmountOfArguments = 1;
            var methodParams = info.GetParameters();
            var actualAmountOfArguments = methodParams.Length;
            if (actualAmountOfArguments != expectedAmountOfArguments)
            {
                throw new NinjectPluginException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongAmountOfArgsToContainerConfigurator,
                        typeof(TConfiguratorType),
                        actualAmountOfArguments,
                        expectedAmountOfArguments),
                    NinjectPluginError.WrongDependenciesSetupMethodSignature);
            }
        }

        private static void CheckExpectedArgumentType<TExpectedArgumentType>(MethodInfo info)
        {
            var actualParameterType = GetActualParameterType<TExpectedArgumentType>(info);
            if (actualParameterType != typeof(TExpectedArgumentType))
            {
                throw new NinjectPluginException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongContainerConfiguratorArgument,
                        typeof(TConfiguratorType),
                        actualParameterType,
                        typeof(TExpectedArgumentType)),
                    NinjectPluginError.WrongDependenciesSetupMethodSignature);
            }
        }

        private static Type GetActualParameterType<TExpectedArgumentType>(MethodInfo info)
        {
            try
            {
                var methodParams = info.GetParameters();
                var argument = methodParams[0];
                var actualParameterType = argument.ParameterType;
                return actualParameterType;
            }
            catch (IndexOutOfRangeException)
            {
                throw new NinjectPluginException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.MissingContainerConfiguratorArgument,
                        typeof(TConfiguratorType),
                        typeof(TExpectedArgumentType)),
                    NinjectPluginError.WrongDependenciesSetupMethodSignature);
            }
        }
    }
}