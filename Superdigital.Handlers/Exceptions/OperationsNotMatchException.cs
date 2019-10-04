using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class OperationsNotMatchException : Exception
    {
        public OperationsNotMatchException()
        {
        }

        public OperationsNotMatchException(string message) : base(message)
        {
        }

        public OperationsNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationsNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}