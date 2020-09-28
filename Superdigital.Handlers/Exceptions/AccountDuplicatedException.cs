using System;
using System.Runtime.Serialization;

namespace Superdigital.Handlers.Exceptions
{
    [Serializable]
    public class AccountDuplicatedException : Exception
    {
        public AccountDuplicatedException()
        {
        }

        public AccountDuplicatedException(string message) : base(message)
        {
        }

        public AccountDuplicatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}