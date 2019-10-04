using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class OperationAlreadyCommitedException : Exception
    {
        public OperationAlreadyCommitedException()
        {
        }

        public OperationAlreadyCommitedException(string message) : base(message)
        {
        }

        public OperationAlreadyCommitedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationAlreadyCommitedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}