using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Threading;
using Tawh.NoTrace.Configuration;
using Tawh.NoTrace.Sessions;
using Tawh.NoTrace.Web.Models.Layout;
using Tawh.NoTrace.Web.Navigation;

namespace Tawh.NoTrace.Web.Controllers
{
    /// <summary>
    /// Layout for 'front end' pages.
    /// </summary>
    public class LayoutController : AbpZeroTemplateControllerBase
    {
        private readonly ISessionAppService _sessionAppService;
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public LayoutController(ISessionAppService sessionAppService, IUserNavigationManager userNavigationManager, IMultiTenancyConfig multiTenancyConfig)
        {
            _sessionAppService = sessionAppService;
            _userNavigationManager = userNavigationManager;
            _multiTenancyConfig = multiTenancyConfig;
        }

        [ChildActionOnly]
        public PartialViewResult Header(string currentPageName = "")
        {
            var headerModel = new HeaderViewModel();
            
            if (AbpSession.UserId.HasValue)
            {
                headerModel.LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            }

            headerModel.Languages = LocalizationManager.GetAllLanguages();
            headerModel.CurrentLanguage = LocalizationManager.CurrentLanguage;
            
            headerModel.Menu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync(FrontEndNavigationProvider.MenuName, AbpSession.UserId));
            headerModel.CurrentPageName = currentPageName;

            headerModel.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            headerModel.TenantRegistrationEnabled = SettingManager.GetSettingValue<bool>(AppSettings.TenantManagement.AllowSelfRegistration);

            return PartialView("~/Views/Layout/_Header.cshtml", headerModel);
        }
    }
}