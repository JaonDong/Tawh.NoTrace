using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Authorization.Users;

namespace Tawh.NoTrace.Configuration.Host.Dto
{
    public class SendTestEmailInput : IInputDto
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}