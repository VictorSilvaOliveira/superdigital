using Superdigital.DataBase;
using System.ComponentModel.DataAnnotations;

namespace Superdigital.Models
{
    public struct AccountDetail : IRegister
    {
        [Required]
        public Account Account { get; set; }
        public object Id { get; set; }
        public Money Balance { get; set; }
    }
}
