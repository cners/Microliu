using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.ViewModels
{
   public class CreateUserModel
    {
        public string NickName { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
