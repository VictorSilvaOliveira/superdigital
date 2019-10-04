using System;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public class AlwaysPositiveAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var money = value as Money;
            return base.IsValid(money) && money.Total > 0;
        }
    }
}