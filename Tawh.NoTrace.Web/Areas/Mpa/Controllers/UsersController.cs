using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Users;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : AbpZeroTemplateControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        private readonly IUserLoginAppService _userLoginAppService;

        public UsersController(
            IUserAppService userAppService,
            UserManager userManager,
            IUserLoginAppService userLoginAppService)
        {
            _userAppService = userAppService;
            _userManager = userManager;
            _userLoginAppService = userLoginAppService;
        }

        public ActionResult Index()
        {
            var model = new UsersViewModel
            {
                FilterText = Request.QueryString["filterText"]
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            var output = await _userAppService.GetUserForEdit(new NullableIdInput<long> { Id = id });
            var viewModel = new CreateOrEditUserModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task<PartialViewResult> PermissionsModal(long id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            var output = await _userAppService.GetUserPermissionsForEdit(new IdInput<long>(id));
            var viewModel = new UserPermissionsEditViewModel(output, user);

            return PartialView("_PermissionsModal", viewModel);
        }

        public async Task<PartialViewResult> LoginAttemptsModal()
        {
            var output = await _userLoginAppService.GetRecentUserLoginAttempts();
            var model = new UserLoginAttemptModalViewModel
            {
                LoginAttempts = output.Items.ToList()
            };
            return PartialView("_LoginAttemptsModal", model);
        }
    }
}