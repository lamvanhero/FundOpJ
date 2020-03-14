using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OppJar.AutoMapper;
using OppJar.Core.Exceptions;
using OppJar.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, configuration)
        {
        }

        public async Task<CommentDto> CreateAsync(CreateEditCommentDto dto)
        {
            var entity = dto.ToEntity();

            var user = await _unitOfWork.UserRepository
                    .FindAll(x => x.Id.Equals(UserId))
                    .Include(x => x.UserDetail)
                    .SingleOrDefaultAsync();

            entity.FullName = $"{user.UserDetail.FirstName} {user.UserDetail.LastName}";
            entity.Email = UserName;
            entity.ProfilePictureUrl = user.UserDetail.Avatar;
            entity.ProfileURL = $"/Account/Settings/{user.Id}";

            _unitOfWork.CommentRepository.Add(entity);

            await _unitOfWork.CommitAsync();

            var result = entity.ToDto();

            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var comment = await _unitOfWork.CommentRepository.FindAll(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (comment == null) throw new NotFoundException($"Not found comment with id {id}");

            _unitOfWork.CommentRepository.Delete(comment);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var comments = await _unitOfWork.CommentRepository.FindAll(x => x.ReferId.Equals(id)).ToListAsync();

            if (comments.Any())
            {
                _unitOfWork.CommentRepository.DeleteRange(comments);

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<PageResultDto<CommentDto>> SearchAsync(CommentQuerySearch querySearch)
        {
            var query = _unitOfWork.CommentRepository
                                .FindAll(x => x.ReferId.Equals(querySearch.ReferId))
                                .OrderByDescending(x => x.CreatedAt);

            var comments = await query.Skip(querySearch.GetSkip())
                                 .Take(querySearch.GetTake())
                                 .ToListAsync();

            return new PageResultDto<CommentDto>(await query.CountAsync(), GetTotalPage(await query.CountAsync(), querySearch.GetTake()), comments.Select(x => x.ToDto()).ToList());
        }

        public async Task<CommentDto> UpdateAsync(string id, CreateEditCommentDto dto)
        {
            var comment = await _unitOfWork.CommentRepository.FindAll(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (comment != null) throw new NotFoundException($"Not found commone with id {id}");

            var entity = dto.ToEntity(comment);

            _unitOfWork.CommentRepository.Update(entity);

            await _unitOfWork.CommitAsync();

            return entity.ToDto();
        }
    }
}
