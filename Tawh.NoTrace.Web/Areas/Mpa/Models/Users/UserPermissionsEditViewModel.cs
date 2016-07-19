using Abp.AutoMapper;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.Authorization.Users.Dto;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Common;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}