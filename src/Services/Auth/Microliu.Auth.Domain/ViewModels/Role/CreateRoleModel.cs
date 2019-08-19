using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.ViewModels
{
    public class CreateRoleModel
    {
        public string RoleName { get; set; }

        public int Sort { get; set; }


        public string CreatorId { get; set; }
    }
}
