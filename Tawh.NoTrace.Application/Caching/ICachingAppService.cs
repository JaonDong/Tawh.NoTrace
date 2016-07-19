using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Caching.Dto;

namespace Tawh.NoTrace.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultOutput<CacheDto> GetAllCaches();

        Task ClearCache(IdInput<string> input);

        Task ClearAllCaches();
    }
}
