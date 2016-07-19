using Abp.Authorization.Roles;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.MultiTenancy;

namespace Tawh.NoTrace.Authorization.Roles
{
    /// <summary>
    /// Represents a role in the system.
    /// </summary>
    public class Role : AbpRole<Tenant, User>
    {
        public Role()
        {
            
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
