using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        const string MESSAGE = "The account dont have money enough to this operation";

        public NotEnoughMoneyException() : base(MESSAGE)
        {
        }

        public NotEnoughMoneyException(Exception innerException) : base(MESSAGE, innerException)
        {
        }

        protected NotEnoughMoneyException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}