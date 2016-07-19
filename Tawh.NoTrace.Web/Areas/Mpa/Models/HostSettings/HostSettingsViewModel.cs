using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Configuration.Host.Dto;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<ComboboxItemDto> EditionItems { get; set; }
    }
}