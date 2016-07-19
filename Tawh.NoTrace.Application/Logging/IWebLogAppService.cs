using Abp.Application.Services;
using Tawh.NoTrace.Dto;
using Tawh.NoTrace.Logging.Dto;

namespace Tawh.NoTrace.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
