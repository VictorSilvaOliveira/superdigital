using System.Collections.Generic;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparables
{
    public class BankComparer : IEqualityComparer<Bank>
    {
        public bool Equals(Bank x, Bank y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Bank obj)
        {
            return obj.GetHashCode();
        }
    }
}
