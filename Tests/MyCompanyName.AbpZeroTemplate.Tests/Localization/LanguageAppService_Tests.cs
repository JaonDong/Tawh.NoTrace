﻿using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Localization;
using Tawh.NoTrace.Localization;
using Tawh.NoTrace.Localization.Dto;
using Tawh.NoTrace.Migrations.Seed;
using Shouldly;
using Xunit;

namespace Tawh.NoTrace.Tests.Localization
{
    public class LanguageAppService_Tests : AppTestBase
    {
        private readonly ILanguageAppService _languageAppService;
        private readonly IApplicationLanguageManager _languageManager;

        public LanguageAppService_Tests()
        {
            LoginAsHostAdmin();

            _languageAppService = Resolve<ILanguageAppService>();
            _languageManager = Resolve<IApplicationLanguageManager>();
        }

        [Fact]
        public async Task Test_GetLanguages()
        {
            //Act
            var output = await _languageAppService.GetLanguages();

            //Assert
            output.Items.Count.ShouldBe(DefaultLanguagesCreator.InitialLanguages.Count);
        }

        [Fact]
        public async Task Create_Language()
        {
            //Act
            var output = await _languageAppService.GetLanguageForEdit(new NullableIdInput(null));

            //Assert
            output.Language.Id.ShouldBeNull();
            output.LanguageNames.Count.ShouldBeGreaterThan(0);
            output.Flags.Count.ShouldBeGreaterThan(0);

            //Arrange
            var currentLanguages = await _languageManager.GetLanguagesAsync(AbpSession.TenantId);
            var nonRegisteredLanguages = output.LanguageNames.Where(l => currentLanguages.All(cl => cl.Name != l.Value)).ToList();

            //Act
            var newLanguageName = nonRegisteredLanguages[RandomHelper.GetRandom(nonRegisteredLanguages.Count)].Value;
            await _languageAppService.CreateOrUpdateLanguage(
                new CreateOrUpdateLanguageInput
                {
                    Language = new ApplicationLanguageEditDto
                    {
                        Icon = output.Flags[RandomHelper.GetRandom(output.Flags.Count)].Value,
                        Name = newLanguageName
                    }
                });

            //Assert
            currentLanguages = await _languageManager.GetLanguagesAsync(AbpSession.TenantId);
            currentLanguages.Any(l => l.Name == newLanguageName).ShouldBeTrue();
        }

        [Fact]
        public async Task Delete_Language()
        {
            //Arrange
            var currentLanguages = await _languageManager.GetLanguagesAsync(AbpSession.TenantId);
            var randomLanguage = RandomHelper.GetRandomOf(currentLanguages.ToArray());

            //Act
            await _languageAppService.DeleteLanguage(new IdInput(randomLanguage.Id));

            //Assert
            currentLanguages = await _languageManager.GetLanguagesAsync(AbpSession.TenantId);
            currentLanguages.Any(l => l.Name == randomLanguage.Name).ShouldBeFalse();
        }

        [Fact]
        public async Task SetDefaultLanguage()
        {
            //Arrange
            var currentLanguages = await _languageManager.GetLanguagesAsync(AbpSession.TenantId);
            var randomLanguage = RandomHelper.GetRandomOf(currentLanguages.ToArray());

            //Act
            await _languageAppService.SetDefaultLanguage(
                new SetDefaultLanguageInput
                {
                    Name = randomLanguage.Name
                });

            //Assert
            var defaultLanguage = await _languageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);

            randomLanguage.ShouldBe(defaultLanguage);
        }

        [Fact]
        public async Task UpdateLanguageText()
        {
            await _languageAppService.UpdateLanguageText(
                new UpdateLanguageTextInput
                {
                    SourceName = AbpZeroTemplateConsts.LocalizationSourceName,
                    LanguageName = "en",
                    Key = "Save",
                    Value = "save-new-value"
                });

            var newValue = Resolve<ILocalizationManager>()
                .GetString(
                    AbpZeroTemplateConsts.LocalizationSourceName,
                    "Save",
                    CultureInfo.GetCultureInfo("en")
                );

            newValue.ShouldBe("save-new-value");
        }
    }
}
