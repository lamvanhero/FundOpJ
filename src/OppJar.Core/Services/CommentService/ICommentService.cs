using OppJar.Dto;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public interface ICommentService
    {
        Task<PageResultDto<CommentDto>> SearchAsync(CommentQuerySearch querySearch);
        Task<CommentDto> CreateAsync(CreateEditCommentDto dto);
        Task<CommentDto> UpdateAsync(string id, CreateEditCommentDto dto);
        Task DeleteAsync(string id);
        Task DeleteByIdAsync(string id);
    }
}
