﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Authorization.Users.Dto
{
    public class UpdateUserPermissionsInput : IInputDto
    {
        [Range(1, int.MaxValue)]
        public long Id { get; set; }

        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}