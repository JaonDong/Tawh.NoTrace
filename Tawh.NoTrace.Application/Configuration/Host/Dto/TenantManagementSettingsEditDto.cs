using Abp.Runtime.Validation;

namespace Tawh.NoTrace.Configuration.Host.Dto
{
    public class TenantManagementSettingsEditDto: IValidate
    {
        public bool AllowSelfRegistration { get; set; }

        public bool IsNewRegisteredTenantActiveByDefault { get; set; }

        public bool UseCaptchaOnRegistration { get; set; }

        public int? DefaultEditionId { get; set; }
    }
}