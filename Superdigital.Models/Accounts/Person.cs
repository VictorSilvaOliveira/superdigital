using Superdigital.Validators;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct Person
    {
        [DigitValidator]
        public PersonalId Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }
    }
}