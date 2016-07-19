using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}