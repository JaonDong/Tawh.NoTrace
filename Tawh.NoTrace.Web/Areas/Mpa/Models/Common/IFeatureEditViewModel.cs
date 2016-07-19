using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Editions.Dto;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}