using System.Collections.Generic;
using Tawh.NoTrace.Authorization.Users.Dto;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}