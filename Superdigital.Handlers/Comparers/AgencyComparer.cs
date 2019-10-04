using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparers
{
    public class AgencyComparer : IEqualityComparer<Models.Agency>
    {
        public bool Equals(Agency me, Agency other)
        {
            return me.Number == other.Number && me.Validador == other.Validador;
        }

        public int GetHashCode(Agency agency)
        {
            return agency.GetHashCode();
        }
    }
}
