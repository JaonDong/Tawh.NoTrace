using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Authorization.Roles.Dto
{
    public class CreateOrUpdateRoleInput : IInputDto
    {
        [Required]
        public RoleEditDto Role { get; set; }

        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}