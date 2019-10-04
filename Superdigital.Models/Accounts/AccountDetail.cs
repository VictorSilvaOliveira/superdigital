using Superdigital.DataBase;
using Superdigital.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Superdigital.Models
{
    public class AccountDetail : IRegister
    {
        [Required]
        public Account Account { get; set; }
        public object Id { get; set; }
        public Money Balance { get; set; }
    }
}
