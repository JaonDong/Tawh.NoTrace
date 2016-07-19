using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Editions;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Editions;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Editions)]
    public class EditionsController : AbpZeroTemplateControllerBase
    {
        private readonly IEditionAppService _editionAppService;

        public EditionsController(IEditionAppService editionAppService)
        {
            _editionAppService = editionAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Editions_Create, AppPermissions.Pages_Editions_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _editionAppService.GetEditionForEdit(new NullableIdInput { Id = id });
            var viewModel = new CreateOrEditEditionModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}