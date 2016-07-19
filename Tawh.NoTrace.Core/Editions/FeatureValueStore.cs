using Abp.Application.Features;
using Tawh.NoTrace.Authorization.Roles;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.MultiTenancy;

namespace Tawh.NoTrace.Editions
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager) 
            : base(tenantManager)
        {
        }
    }
}
