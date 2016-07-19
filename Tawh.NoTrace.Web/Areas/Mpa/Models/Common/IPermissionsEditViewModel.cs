using System.Collections.Generic;
using Tawh.NoTrace.Authorization.Dto;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}