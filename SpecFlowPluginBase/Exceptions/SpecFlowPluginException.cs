namespace SpecFlowPluginBase.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SpecFlowPluginException : Exception
    {
        public SpecFlowPluginException()
        {
        }

        public SpecFlowPluginException(string message)
            : base(message)
        {
        }

        public SpecFlowPluginException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public SpecFlowPluginException(SpecFlowPluginError error)
        {
            this.Error = error;
        }

        public SpecFlowPluginException(string message, SpecFlowPluginError error)
            : base(message)
        {
            this.Error = error;
        }

        public SpecFlowPluginException(string message, Exception inner, SpecFlowPluginError error)
            : base(message, inner)
        {
            this.Error = error;
        }

        protected SpecFlowPluginException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SpecFlowPluginError Error { get; }
    }
}