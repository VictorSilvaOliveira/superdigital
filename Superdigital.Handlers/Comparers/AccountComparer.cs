using System.Collections.Generic;

namespace Superdigital.Handlers.Comparers
{
    public class AccountComparer : IEqualityComparer<Models.Account>
    {
        public bool Equals(Models.Account x, Models.Account y)
        {
            return x.Bank.IsSame(y.Bank) &&
                   x.AccountNumber.IsSame(y.AccountNumber) &&
                   x.Agency.IsSame(y.Agency);
        }

        public int GetHashCode(Models.Account obj)
        {
            return obj.GetHashCode();
        }
    }
}
