using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Editions;
using Tawh.NoTrace.MultiTenancy;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Tenants;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantsController : AbpZeroTemplateControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly TenantManager _tenantManager;
        private readonly IEditionAppService _editionAppService;

        public TenantsController(
            ITenantAppService tenantAppService, 
            TenantManager tenantManager, 
            IEditionAppService editionAppService)
        {
            _tenantAppService = tenantAppService;
            _tenantManager = tenantManager;
            _editionAppService = editionAppService;
        }

        public ActionResult Index()
        {
            ViewBag.FilterText = Request.QueryString["filterText"];
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var editionItems = await _editionAppService.GetEditionComboboxItems();
            var viewModel = new CreateTenantViewModel(editionItems);

            return PartialView("_CreateModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<PartialViewResult> EditModal(int id)
        {
            var tenantEditDto = await _tenantAppService.GetTenantForEdit(new EntityRequestInput(id));
            var editionItems = await _editionAppService.GetEditionComboboxItems(tenantEditDto.EditionId);
            var viewModel = new EditTenantViewModel(tenantEditDto, editionItems);

            return PartialView("_EditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<PartialViewResult> FeaturesModal(int id)
        {
            var tenant = await _tenantManager.GetByIdAsync(id);
            var output = await _tenantAppService.GetTenantFeaturesForEdit(new EntityRequestInput(id));
            var viewModel = new TenantFeaturesEditViewModel(tenant, output);

            return PartialView("_FeaturesModal", viewModel);
        }
    }
}