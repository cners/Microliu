using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SharingProject.MultiTenancy.Dto;

namespace SharingProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

