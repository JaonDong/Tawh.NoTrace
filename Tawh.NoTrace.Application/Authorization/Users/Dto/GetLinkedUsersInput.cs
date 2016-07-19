using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Tawh.NoTrace.Authorization.Users.Dto
{
    public class GetLinkedUsersInput : IInputDto, IPagedResultRequest, ISortedResultRequest, IShouldNormalize
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,Surname";
            }
        }
    }
}