using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct Bank
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}