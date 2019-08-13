

namespace Microliu.Auth.Domain
{
    public partial class AuthSupervisor : IAuthSuperVisor
    {
        private readonly IRoleRepository _roleRepository;


        public AuthSupervisor(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;// 角色
        }
    }
}
