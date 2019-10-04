using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public class Bank
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}