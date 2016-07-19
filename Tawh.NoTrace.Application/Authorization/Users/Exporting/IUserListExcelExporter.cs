using System.Collections.Generic;
using Tawh.NoTrace.Authorization.Users.Dto;
using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}