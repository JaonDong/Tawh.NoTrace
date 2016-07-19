using System.Threading.Tasks;
using Abp.Application.Services;
using Tawh.NoTrace.Sessions.Dto;

namespace Tawh.NoTrace.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
