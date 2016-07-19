using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Editions.Dto
{
    public class GetEditionForEditOutput : IOutputDto
    {
        public EditionEditDto Edition { get; set; }

        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}