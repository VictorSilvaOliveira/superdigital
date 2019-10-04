using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparers
{
    public class AccountComparer : IEqualityComparer<Models.Account>
    {
        public bool Equals(Models.Account me, Models.Account other)
        {
            return me.Bank.IsSame(other.Bank) &&
                   me.AccountNumber.IsSame(other.AccountNumber) &&
                   me.Agency.IsSame(other.Agency);
        }

        public int GetHashCode(Models.Account account)
        {
            return account.GetHashCode();
        }
    }
}
