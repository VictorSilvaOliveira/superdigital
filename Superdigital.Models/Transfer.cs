using Superdigital.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Superdigital.Models
{
    public class Transfer
    {
        [Required]
        public Account Origin { get; set; }

        [Required]
        public Account Destiny { get; set; }

        [AlwaysPositive]
        public Money Ammount { set; get; }

    }
}
