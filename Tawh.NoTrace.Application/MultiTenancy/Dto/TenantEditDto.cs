﻿using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Tawh.NoTrace.MultiTenancy.Dto
{
    [AutoMap(typeof (Tenant))]
    public class TenantEditDto : EntityDto, IDoubleWayDto
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }
    }
}