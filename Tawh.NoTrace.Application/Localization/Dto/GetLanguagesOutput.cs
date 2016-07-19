using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Localization.Dto
{
    public class GetLanguagesOutput : ListResultOutput<ApplicationLanguageListDto>
    {
        public string DefaultLanguageName { get; set; }

        public GetLanguagesOutput()
        {
            
        }

        public GetLanguagesOutput(IReadOnlyList<ApplicationLanguageListDto> items, string defaultLanguageName)
            : base(items)
        {
            DefaultLanguageName = defaultLanguageName;
        }
    }
}