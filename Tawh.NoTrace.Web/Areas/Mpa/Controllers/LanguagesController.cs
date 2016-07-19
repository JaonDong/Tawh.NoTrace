using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Localization;
using Abp.Web.Mvc.Authorization;
using Tawh.NoTrace.Authorization;
using Tawh.NoTrace.Localization;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Languages;
using Tawh.NoTrace.Web.Controllers;

namespace Tawh.NoTrace.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Languages)]
    public class LanguagesController : AbpZeroTemplateControllerBase
    {
        private readonly ILanguageAppService _languageAppService;
        private readonly ILanguageManager _languageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;

        public LanguagesController(
            ILanguageAppService languageAppService,
            ILanguageManager languageManager,
            IApplicationLanguageTextManager applicationLanguageTextManager)
        {
            _languageAppService = languageAppService;
            _languageManager = languageManager;
            _applicationLanguageTextManager = applicationLanguageTextManager;
        }

        public ActionResult Index()
        {
            var viewModel = new LanguagesIndexViewModel
            {
                IsTenantView = AbpSession.TenantId.HasValue
            };

            return View(viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Languages_Create, AppPermissions.Pages_Administration_Languages_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _languageAppService.GetLanguageForEdit(new NullableIdInput { Id = id });
            var viewModel = new CreateOrEditLanguageModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Languages_ChangeTexts)]
        public ActionResult Texts(
            string languageName, 
            string sourceName = "", 
            string baseLanguageName = "", 
            string targetValueFilter = "ALL", 
            string filterText = "")
        {
            //Normalize arguments
            if (sourceName.IsNullOrEmpty())
            {
                sourceName = "AbpZeroTemplate";
            }

            if (baseLanguageName.IsNullOrEmpty())
            {
                baseLanguageName = LocalizationManager.CurrentLanguage.Name;
            }

            //Create view model
            var viewModel = new LanguageTextsViewModel();
            
            viewModel.LanguageName = languageName;

            viewModel.Languages = LocalizationManager.GetAllLanguages().ToList();

            viewModel.Sources = LocalizationManager
                .GetAllSources()
                .Where(s => s.GetType() == typeof (MultiTenantLocalizationSource))
                .Select(s => new SelectListItem()
                {
                    Value = s.Name, 
                    Text = s.Name, 
                    Selected = s.Name == sourceName
                })
                .ToList();

            viewModel.BaseLanguageName = baseLanguageName;

            viewModel.TargetValueFilter = targetValueFilter;
            viewModel.FilterText = filterText;

            return View(viewModel);            
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Languages_ChangeTexts)]
        public PartialViewResult EditTextModal(
            string sourceName, 
            string baseLanguageName, 
            string languageName,
            string key)
        {
            var languages = _languageManager.GetLanguages();

            var baselanguage = languages.FirstOrDefault(l => l.Name == baseLanguageName);
            if (baselanguage == null)
            {
                throw new ApplicationException("Could not find language: " + baseLanguageName);
            }

            var targetLanguage = languages.FirstOrDefault(l => l.Name == languageName);
            if (targetLanguage == null)
            {
                throw new ApplicationException("Could not find language: " + languageName);
            }

            var baseText = _applicationLanguageTextManager.GetStringOrNull(
                AbpSession.TenantId,
                sourceName,
                CultureInfo.GetCultureInfo(baseLanguageName),
                key
                );

            var targetText = _applicationLanguageTextManager.GetStringOrNull(
                AbpSession.TenantId,
                sourceName,
                CultureInfo.GetCultureInfo(languageName),
                key,
                false
                );

            var viewModel = new EditTextModalViewModel
            {
                SourceName = sourceName,
                BaseLanguage = baselanguage,
                TargetLanguage = targetLanguage,
                BaseText = baseText,
                TargetText = targetText,
                Key = key
            };

            return PartialView("_EditTextModal", viewModel);
        }
    }
}