using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Tawh.NoTrace.Editions.Dto;

namespace Tawh.NoTrace.Editions
{
    public interface IEditionAppService : IApplicationService
    {
        Task<ListResultOutput<EditionListDto>> GetEditions();

        Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdInput input);

        Task CreateOrUpdateEdition(CreateOrUpdateEditionDto input);

        Task DeleteEdition(EntityRequestInput input);

        Task<List<ComboboxItemDto>> GetEditionComboboxItems(int? selectedEditionId = null);
    }
}