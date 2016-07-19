﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Localization.Dto
{
    public class GetLanguageForEditOutput : IOutputDto
    {
        public ApplicationLanguageEditDto Language { get; set; }

        public List<ComboboxItemDto> LanguageNames { get; set; }
        
        public List<ComboboxItemDto> Flags { get; set; }

        public GetLanguageForEditOutput()
        {
            LanguageNames = new List<ComboboxItemDto>();
            Flags = new List<ComboboxItemDto>();
        }
    }
}