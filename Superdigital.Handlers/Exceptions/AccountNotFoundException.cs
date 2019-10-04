using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class AccountNotFoundException : Exception
    {
        const string MESSAGE = "The account was not founded";

        public AccountNotFoundException() : base(MESSAGE)
        {
        }

        public AccountNotFoundException(Exception innerException) : base(MESSAGE, innerException)
        {
        }

        protected AccountNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}