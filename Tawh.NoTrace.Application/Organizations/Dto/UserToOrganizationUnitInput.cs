using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Organizations.Dto
{
    public class UserToOrganizationUnitInput : IInputDto
    {
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}