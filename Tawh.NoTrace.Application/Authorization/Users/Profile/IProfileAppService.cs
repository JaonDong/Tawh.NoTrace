using System.Threading.Tasks;
using Abp.Application.Services;
using Tawh.NoTrace.Authorization.Users.Profile.Dto;

namespace Tawh.NoTrace.Authorization.Users.Profile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();

        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        
        Task ChangePassword(ChangePasswordInput input);

        Task UpdateProfilePicture(UpdateProfilePictureInput input);
    }
}
