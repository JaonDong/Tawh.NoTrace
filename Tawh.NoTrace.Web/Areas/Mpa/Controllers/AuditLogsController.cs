using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogsController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}