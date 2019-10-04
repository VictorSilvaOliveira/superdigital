using Superdigital.Models;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Validators
{
    public class DigitValidatorAttribute : RequiredAttribute
    {
        public DigitValidatorAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var number = value as NumberValidator;
            return base.IsValid(number) && number.IsValid();
        }
    }
}
