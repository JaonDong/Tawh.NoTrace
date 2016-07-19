using System.Collections.Generic;

namespace Tawh.NoTrace.Tenants.Dashboard.Dto
{
    public class GetMemberActivityOutput
    {
        public List<int> TotalMembers { get; set; }

        public List<int> NewMembers { get; set; }
    }
}