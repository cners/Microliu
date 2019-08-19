using Abp.Authorization;
using SharingProject.Authorization.Roles;
using SharingProject.Authorization.Users;

namespace SharingProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
