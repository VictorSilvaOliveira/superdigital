using Superdigital.Validators;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct Account
    {
        [Required]
        public Bank Bank { get; set; }

        [DigitValidator]
        public Agency Agency { get; set; }

        [DigitValidator]
        public AccountNumber AccountNumber { get; set; }

        [Required]
        public Person Owner { get; set; }
    }
}
