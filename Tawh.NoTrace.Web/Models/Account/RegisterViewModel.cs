using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.MultiTenancy;

namespace Tawh.NoTrace.Web.Models.Account
{
    public class RegisterViewModel : IInputDto
    {
        /// <summary>
        /// Not required for single-tenant applications.
        /// </summary>
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        public bool IsExternalLogin { get; set; }
    }
}