using Abp.Runtime.Validation;

namespace Tawh.NoTrace.Configuration.Host.Dto
{
    public class HostUserManagementSettingsEditDto : IValidate
    {
        public bool IsEmailConfirmationRequiredForLogin { get; set; }
    }
}