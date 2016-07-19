using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Tawh.NoTrace.Localization.Dto
{
    public class CreateOrUpdateLanguageInput : IInputDto
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}