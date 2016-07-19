using System.Web.Mvc;

namespace Tawh.NoTrace.Web.Controllers
{
    public class HomeController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}