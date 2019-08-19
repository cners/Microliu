using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.Domain.Converters
{
    public class RoleConverter
    {
        public static Role ToRole(CreateRoleModel model)
        {
            var role = new Role();
            role.Id = Guid.NewGuid().ToString("N").ToUpper();
            role.CreateTime = DateTimeOffset.Now;
            role.Creator = "";//model.CreatorId
            role.IsDeleted = 1;
            role.IsEnabled = 1;

            role.RoleName = model.RoleName;
            return role;
        }
    }
}
