using System.Collections.Generic;
using Tawh.NoTrace.Caching.Dto;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}