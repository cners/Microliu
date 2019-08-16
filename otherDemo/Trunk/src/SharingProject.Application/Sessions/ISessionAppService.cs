using System.Threading.Tasks;
using Abp.Application.Services;
using SharingProject.Sessions.Dto;

namespace SharingProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
