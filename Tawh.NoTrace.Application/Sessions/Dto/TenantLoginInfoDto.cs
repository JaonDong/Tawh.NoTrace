﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Tawh.NoTrace.MultiTenancy;

namespace Tawh.NoTrace.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public string EditionDisplayName { get; set; }
    }
}