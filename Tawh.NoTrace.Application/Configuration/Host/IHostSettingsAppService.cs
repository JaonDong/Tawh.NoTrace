using System.Threading.Tasks;
using Abp.Application.Services;
using Tawh.NoTrace.Configuration.Host.Dto;

namespace Tawh.NoTrace.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
