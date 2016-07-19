using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Authorization.Users.Dto
{
    public class UnlinkUserInput : IInputDto
    {
        public long UserId { get; set; }
    }
}