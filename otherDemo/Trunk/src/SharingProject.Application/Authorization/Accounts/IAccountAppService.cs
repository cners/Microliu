using System.Threading.Tasks;
using Abp.Application.Services;
using SharingProject.Authorization.Accounts.Dto;

namespace SharingProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
