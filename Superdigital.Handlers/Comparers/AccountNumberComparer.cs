using System.Collections.Generic;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparers
{
    public class AccountNumberComparer : IEqualityComparer<AccountNumber>
    {
        public bool Equals(AccountNumber x, AccountNumber y)
        {
            return x.Number == y.Number && x.Validador == y.Validador;
        }

        public int GetHashCode(AccountNumber obj)
        {
            return obj.GetHashCode();
        }
    }
}
