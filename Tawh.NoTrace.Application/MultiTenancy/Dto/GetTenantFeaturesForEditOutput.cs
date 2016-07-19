using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Editions.Dto;

namespace Tawh.NoTrace.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput : IOutputDto
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}