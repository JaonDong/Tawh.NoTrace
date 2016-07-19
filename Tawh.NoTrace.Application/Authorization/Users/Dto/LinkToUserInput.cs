using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Authorization.Users.Dto
{
    public class LinkToUserInput : IInputDto
    {
        public string TenancyName { get; set; }

        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}