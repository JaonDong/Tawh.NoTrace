using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.MultiTenancy;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AbpZeroTemplateControllerBase
    {
        public async Task<ActionResult> Index()
        {
            if (AbpSession.MultiTenancySide == MultiTenancySides.Host)
            {
                if (await IsGrantedAsync(AppPermissions.Pages_Tenants))
                {
                    return RedirectToAction("Index", "Tenants");
                }
            }
            else
            {
                if (await IsGrantedAsync(AppPermissions.Pages_Tenant_Dashboard))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            //Default page if no permission to the pages above
            return RedirectToAction("Index", "Welcome");
        }
    }
}