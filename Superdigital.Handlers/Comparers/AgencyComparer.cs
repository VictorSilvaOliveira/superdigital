using Superdigital.Models;
using System.Collections.Generic;

namespace Superdigital.Handlers.Comparers
{
    public class AgencyComparer : IEqualityComparer<Agency>
    {
        public bool Equals(Agency x, Agency y)
        {
            return x.Number == y.Number && x.Validador == y.Validador;
        }

        public int GetHashCode(Agency y)
        {
            return y.GetHashCode();
        }
    }
}
