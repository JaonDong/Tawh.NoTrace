using Abp.AutoMapper;
using Tawh.NoTrace.Editions.Dto;
using Tawh.NoTrace.Web.Areas.Mpa.Models.Common;

namespace Tawh.NoTrace.Web.Areas.Mpa.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionForEditOutput))]
    public class CreateOrEditEditionModalViewModel : GetEditionForEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode
        {
            get { return Edition.Id.HasValue; }
        }

        public CreateOrEditEditionModalViewModel(GetEditionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}