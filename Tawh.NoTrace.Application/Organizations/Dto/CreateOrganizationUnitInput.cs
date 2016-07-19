using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Organizations;

namespace Tawh.NoTrace.Organizations.Dto
{
    public class CreateOrganizationUnitInput : IInputDto
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; } 
    }
}