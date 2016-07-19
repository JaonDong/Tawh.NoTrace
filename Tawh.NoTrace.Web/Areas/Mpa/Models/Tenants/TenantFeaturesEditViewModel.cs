using Abp.AutoMapper;
using Tawh.NoTrace.MultiTenancy;
using Tawh.NoTrace.MultiTenancy.Dto;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Common;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesForEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesForEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}