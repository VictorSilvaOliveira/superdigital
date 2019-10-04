using Superdigital.Validators;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public class Person
    {
        [DigitValidator]
        public PersonalId Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }
    }
}