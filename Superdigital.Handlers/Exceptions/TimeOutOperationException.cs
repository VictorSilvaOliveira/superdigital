using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class TimeOutOperationException : Exception
    {
        const string MESSAGE = "The operation take too long to execute";

        public TimeOutOperationException() : base(MESSAGE)
        {
        }

        public TimeOutOperationException(Exception innerException) : base(MESSAGE, innerException)
        {
        }

        protected TimeOutOperationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {

        }
    }
}