using Abp.Application.Services;
using Tawh.NoTrace.Tenants.Dashboard.Dto;

namespace Tawh.NoTrace.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
