namespace Ninject.SpecFlowPlugin.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NinjectPluginException : Exception
    {
        public NinjectPluginException()
        {
        }

        public NinjectPluginException(string message)
            : base(message)
        {
        }

        public NinjectPluginException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public NinjectPluginException(NinjectPluginError error)
        {
            this.Error = error;
        }

        public NinjectPluginException(string message, NinjectPluginError error)
            : base(message)
        {
            this.Error = error;
        }

        public NinjectPluginException(string message, Exception inner, NinjectPluginError error)
            : base(message, inner)
        {
            this.Error = error;
        }

        protected NinjectPluginException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public NinjectPluginError Error { get; }
    }
}