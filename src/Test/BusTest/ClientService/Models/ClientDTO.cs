﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientService.Models
{
    public class ClientDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Client name cannot be null!")]
        [MaxLength(20, ErrorMessage = "Client name is too long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Client sex cannot be null!")]
        [MaxLength(1, ErrorMessage = "Client sex only select in M and F!")]
        public string Sex { get; set; }
    }
}
