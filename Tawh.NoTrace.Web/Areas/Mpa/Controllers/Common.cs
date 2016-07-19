using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Common.Modals;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : AbpZeroTemplateControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}