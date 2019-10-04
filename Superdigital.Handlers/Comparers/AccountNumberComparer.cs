using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparers
{
    public class AccountNumberComparer : IEqualityComparer<AccountNumber>
    {
        public bool Equals(AccountNumber me, AccountNumber other)
        {
            return me.Number == other.Number && me.Validador == other.Validador;
        }

        public int GetHashCode(AccountNumber accountNumber)
        {
            return accountNumber.GetHashCode();
        }
    }
}
