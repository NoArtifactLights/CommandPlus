using System;
using System.Runtime.Serialization;

namespace CommandPlus
{
    [Serializable]
    class UnknownCommandException : Exception
    {
        public UnknownCommandException()
        {
        }

        public UnknownCommandException(string commandName) : base("Unknown command " + commandName)
        {
        }

        public UnknownCommandException(string commandName, int parameters) : base("Unknown command " + commandName + " with " + parameters + " parameters.")
        {
        }

        public UnknownCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}