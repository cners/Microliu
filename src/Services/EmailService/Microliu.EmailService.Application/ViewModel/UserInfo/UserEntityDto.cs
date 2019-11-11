using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
    public class UserEntityDto
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public UserEntityDto()
        {
            Id = "";
            Email = "";
        }
    }
}
