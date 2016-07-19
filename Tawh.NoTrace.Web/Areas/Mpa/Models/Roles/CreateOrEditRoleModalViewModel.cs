using Abp.AutoMapper;
using Tawh.NoTrace.Authorization.Roles.Dto;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Common;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode
        {
            get { return Role.Id.HasValue; }
        }

        public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}