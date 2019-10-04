using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class OperationNotRevertedException : Exception
    {
        public OperationNotRevertedException()
        {
        }

        public OperationNotRevertedException(string message) : base(message)
        {
        }

        public OperationNotRevertedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationNotRevertedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}