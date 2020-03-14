using OppJar.Common.Enum;
using OppJar.Dto;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public interface IMemoryService
    {
        Task<FeedDto> CreateFeedAsync(CreateFeedDto dto);
        Task<PageResultDto<FeedDto>> SearchAsync(FeedQuerySearch querySearch);
        Task<FeedDto> GetByIdAsync(string id, string slug);
        Task<FeedDto> ChangePrivacyByIdAsync(string id, Privacy privacy = Privacy.Private);
        Task DeleteByIdAsync(string id);
        Task LikeAsync(string id);
    }
}
