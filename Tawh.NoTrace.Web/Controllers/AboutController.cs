using System.Web.Mvc;

namespace Tawh.NoTrace.Web.Controllers
{
    public class AboutController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}