using Superdigital.Handlers.Comparables;
using Superdigital.Handlers.Comparers;
using Superdigital.Handlers.Exceptions;
using Superdigital.Models;
using System.Collections.Generic;
using System.Linq;

namespace Superdigital.Handlers
{
    internal static class LibraryExtensions
    {
        internal static void ValidateOperations(this IEnumerable<Operation> operations)
        {

            if (operations.Count() > 2)
            {
                throw new InvalidNumberOfOperationsException();
            }

            if (operations.Count() == 2 && operations.First().OperationType.Equals(operations.Last().OperationType))
            {
                throw new OperationsNotMatchException();
            }

        }

        internal static bool IsSame(this Bank me, Bank other)
        {
            var comparer = new BankComparer();

            return comparer.Equals(me, other);
        }


        internal static bool IsSame(this Agency me, Agency other)
        {
            var comparer = new AgencyComparer();

            return comparer.Equals(me, other);
        }

        internal static bool IsSame(this AccountNumber me, AccountNumber other)
        {
            var comparer = new AccountNumberComparer();

            return comparer.Equals(me, other);
        }

        internal static bool IsSame(this Models.Account me, Models.Account other)
        {
            var comparer = new AccountComparer();

            return comparer.Equals(me, other);
        }
    }
}
