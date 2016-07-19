using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Authorization.Users.Dto;

namespace Tawh.NoTrace.Authorization.Users
{
    public interface IUserLinkAppService : IApplicationService
    {
        Task LinkToUser(LinkToUserInput linkToUserInput);

        Task<PagedResultOutput<LinkedUserDto>> GetLinkedUsers(GetLinkedUsersInput input);

        Task<ListResultOutput<LinkedUserDto>> GetRecentlyUsedLinkedUsers();

        Task UnlinkUser(UnlinkUserInput input);
    }
}
