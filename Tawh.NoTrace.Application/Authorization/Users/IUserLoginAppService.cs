using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Authorization.Users.Dto;

namespace Tawh.NoTrace.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultOutput<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
