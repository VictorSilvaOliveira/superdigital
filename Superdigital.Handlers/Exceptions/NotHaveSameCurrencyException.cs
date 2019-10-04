using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class NotHaveSameCurrencyException : Exception
    {
        public NotHaveSameCurrencyException()
        {
        }

        public NotHaveSameCurrencyException(string message) : base(message)
        {
        }

        public NotHaveSameCurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotHaveSameCurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}