using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}