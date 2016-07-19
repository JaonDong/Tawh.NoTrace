using System.Threading.Tasks;
using Abp.Application.Services;
using Tawh.NoTrace.Configuration.Tenants.Dto;

namespace Tawh.NoTrace.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);
    }
}
