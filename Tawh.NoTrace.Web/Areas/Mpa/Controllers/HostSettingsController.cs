using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.Configuration.Host;
using Tawh.NoTrace.Editions;
using Tawh.NoTrace.Web.Areas.Mpa.Models.HostSettings;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Settings)]
    public class HostSettingsController : AbpZeroTemplateControllerBase
    {
        private readonly UserManager _userManager;
        private readonly IHostSettingsAppService _hostSettingsAppService;
        private readonly IEditionAppService _editionAppService;

        public HostSettingsController(
            IHostSettingsAppService hostSettingsAppService,
            UserManager userManager, 
            IEditionAppService editionAppService)
        {
            _hostSettingsAppService = hostSettingsAppService;
            _userManager = userManager;
            _editionAppService = editionAppService;
        }

        public async Task<ActionResult> Index()
        {
            var hostSettings = await _hostSettingsAppService.GetAllSettings();
            var editionItems = await _editionAppService.GetEditionComboboxItems(hostSettings.TenantManagement.DefaultEditionId);
            ViewBag.CurrentUserEmail = await _userManager.GetEmailAsync(AbpSession.GetUserId());

            var model = new HostSettingsViewModel
            {
                Settings = hostSettings,
                EditionItems = editionItems
            };

            return View(model);
        }
    }
}