using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Common.Dto;

namespace Tawh.NoTrace.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultOutput<ComboboxItemDto>> GetEditionsForCombobox();

        Task<PagedResultOutput<NameValueDto>> FindUsers(FindUsersInput input);
    }
}