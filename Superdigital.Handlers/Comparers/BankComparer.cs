using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Superdigital.Models;

namespace Superdigital.Handlers.Comparables
{
    public class BankComparer : IEqualityComparer<Bank>
    {
        public bool Equals(Bank me, Bank other)
        {
            return me.Id == other.Id;
        }

        public int GetHashCode(Bank bank)
        {
            return bank.GetHashCode();
        }
    }
}
