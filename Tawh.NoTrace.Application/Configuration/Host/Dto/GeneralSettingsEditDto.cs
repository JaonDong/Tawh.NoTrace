using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace Tawh.NoTrace.Configuration.Host.Dto
{
    public class GeneralSettingsEditDto : IValidate
    {
        [MaxLength(128)]
        public string WebSiteRootAddress { get; set; }
    }
}