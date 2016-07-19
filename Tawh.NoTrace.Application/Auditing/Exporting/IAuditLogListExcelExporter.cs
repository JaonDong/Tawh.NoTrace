using System.Collections.Generic;
using Tawh.NoTrace.Auditing.Dto;
using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
