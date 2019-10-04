using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class InvalidNumberOfOperationsException : Exception
    {
        public InvalidNumberOfOperationsException()
        {
        }

        public InvalidNumberOfOperationsException(string message) : base(message)
        {
        }

        public InvalidNumberOfOperationsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidNumberOfOperationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}