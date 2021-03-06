﻿using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct Money
    {
        [Required]
        public double Total { get; set; }
        [Required]
        public string Currency { get; set; }


        public static Money operator +(Money me, Money other)
        {
            Money sum = default(Money);
            if (me.Currency == other.Currency)
            {
                sum = new Money();
                sum.Currency = me.Currency;
                sum.Total = me.Total + other.Total;
            }

            return sum;
        }

        public static Money operator -(Money me, Money other)
        {
            Money diff = default(Money);
            if (me.Currency == other.Currency)
            {
                diff = new Money();
                diff.Currency = me.Currency;
                diff.Total = me.Total - other.Total;
            }

            return diff;
        }
    }
}