using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.Extensions
{
    public class EmailServiceSettings
    {
        public EmailSender Sender { get; set; }
        public class EmailSender
        {
            public string Name { get; set; }

            public string Password { get; set; }

            public string Host { get; set; }

            public int Port { get; set; }
        }
    }
}
