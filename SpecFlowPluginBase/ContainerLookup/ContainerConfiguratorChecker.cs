namespace SpecFlowPluginBase.ContainerLookup
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using SpecFlowPluginBase.Attributes;
    using SpecFlowPluginBase.Exceptions;
    using SpecFlowPluginBase.Properties;

    internal static class ContainerConfiguratorChecker<TConfiguratorType, TContainerType>
        where TConfiguratorType : ContainerConfigurationAttribute
        where TContainerType : class
    {
        public static void CheckMethodSignature(MethodInfo info)
        {
            CheckExpectedReturnType(info);
            CheckSingleArgumentIsExpected(info);
            CheckExpectedArgumentType<TContainerType>(info);
        }

        private static void CheckExpectedReturnType(MethodInfo info)
        {
            var actualReturnType = info.ReturnType;
            if (actualReturnType != typeof(void))
            {
                throw new WrongContainerSetupSignatureException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongContainerConfiguratorReturnType,
                        typeof(TConfiguratorType),
                        actualReturnType));
            }
        }

        private static void CheckSingleArgumentIsExpected(MethodInfo info)
        {
            const int expectedAmountOfArguments = 1;
            var methodParams = info.GetParameters();
            var actualAmountOfArguments = methodParams.Length;
            if (actualAmountOfArguments != expectedAmountOfArguments)
            {
                throw new WrongContainerSetupSignatureException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongAmountOfArgsToContainerConfigurator,
                        typeof(TConfiguratorType),
                        actualAmountOfArguments,
                        expectedAmountOfArguments));
            }
        }

        private static void CheckExpectedArgumentType<TExpectedArgumentType>(MethodInfo info)
        {
            var actualParameterType = GetActualParameterType<TExpectedArgumentType>(info);
            if (actualParameterType != typeof(TExpectedArgumentType))
            {
                throw new WrongContainerSetupSignatureException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.WrongContainerConfiguratorArgument,
                        typeof(TConfiguratorType),
                        actualParameterType,
                        typeof(TExpectedArgumentType)));
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
                throw new WrongContainerSetupSignatureException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.MissingContainerConfiguratorArgument,
                        typeof(TConfiguratorType),
                        typeof(TExpectedArgumentType)));
            }
        }
    }
}