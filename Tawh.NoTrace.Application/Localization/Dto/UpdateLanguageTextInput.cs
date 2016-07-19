using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Localization;

namespace Tawh.NoTrace.Localization.Dto
{
    public class UpdateLanguageTextInput : IInputDto
    {
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public string LanguageName { get; set; }

        [Required]
        [StringLength(ApplicationLanguageText.MaxSourceNameLength)]
        public string SourceName { get; set; }

        [Required]
        [StringLength(ApplicationLanguageText.MaxKeyLength)]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(ApplicationLanguageText.MaxValueLength)]
        public string Value { get; set; }
    }
}