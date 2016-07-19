using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Auditing.Dto;
using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultOutput<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}