using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct Transfer
    {
        [Required]
        public Account Origin { get; set; }

        [Required]
        public Account Destiny { get; set; }

        [AlwaysPositive]
        public Money Ammount { set; get; }

    }
}
