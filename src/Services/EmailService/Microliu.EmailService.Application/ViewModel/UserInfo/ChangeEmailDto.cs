using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
    public class ChangeEmailDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string NewEmail { get; set; }
    }
}
