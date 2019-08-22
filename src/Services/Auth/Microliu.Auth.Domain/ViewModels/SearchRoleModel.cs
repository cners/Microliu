using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain
{
    public class SearchRoleModel:BaseByPageQueryModel
    {
        public string RoleName { get; set; }
    }
}
