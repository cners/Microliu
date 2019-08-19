using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microliu.Test.EFCoreCodeFirst.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(32), Required]
        public string Account { get; set; }

        [MaxLength(32), Required]
        public string Password { get; set; }
    }
}
