using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OppJar.AutoMapper;
using OppJar.Common.Enum;
using OppJar.Core.Exceptions;
using OppJar.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public class MemoryService : ServiceBase, IMemoryService
    {

        public MemoryService(IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork, configuration) { }

        public async Task<FeedDto> ChangePrivacyByIdAsync(string id, Privacy privacy = Privacy.Private)
        {
            var feed = await _unitOfWork.FeedRepository.FindByAsync(id);

            if (feed == null) throw new NotFoundException("Not found.");

            feed.Privacy = privacy;

            _unitOfWork.FeedRepository.Update(feed);

            await _unitOfWork.CommitAsync();

            return feed.ToDto();
        }

        public async Task<FeedDto> CreateFeedAsync(CreateFeedDto dto)
        {
            var entity = dto.ToEntity();

            _unitOfWork.FeedRepository.Add(entity);

            await _unitOfWork.CommitAsync();

            var result = entity.ToDto();

            var user = await _unitOfWork.UserRepository
                                    .FindAll(x => x.Id.Equals(result.CreatedBy))
                                    .Include(x => x.UserDetail)
                                    .SingleOrDefaultAsync();

            result.FullName = $"{user?.UserDetail?.FirstName} {user?.UserDetail?.LastName}";
            result.AvatarUrl = user?.UserDetail?.Avatar;

            return result;
        }

        public async Task DeleteByIdAsync(string id)
        {
            var entity = await _unitOfWork.FeedRepository.FindByAsync(id);

            if (entity == null) throw new Exception("Not found feed with id: " + id);

            _unitOfWork.FeedRepository.Delete(entity);

            await _unitOfWork.CommitAsync();
        }

        public async Task<FeedDto> GetByIdAsync(string id, string slug)
        {
            var feed = await _unitOfWork.FeedRepository
                                    .FindAll(x => x.Id.Equals(id))
                                    .SingleOrDefaultAsync();

            if (feed == null) throw new NotFoundException("Not found.");

            var result = feed.ToDto();

            var user = await _unitOfWork.UserRepository
                                    .FindAll(x => x.Id.Equals(result.CreatedBy))
                                    .Include(x => x.UserDetail)
                                    .SingleOrDefaultAsync();

            result.FullName = $"{user.UserDetail.FirstName} {user.UserDetail.LastName}";
            result.AvatarUrl = user.UserDetail.Avatar;
            result.UserSlug = slug;

            return result;
        }

        public async Task LikeAsync(string id)
        {
            var entity = await _unitOfWork.FeedRepository.FindByAsync(id);

            if (entity == null) throw new Exception("Not found feed with id: " + id);

            entity.Like += 1;

            _unitOfWork.FeedRepository.Update(entity);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PageResultDto<FeedDto>> SearchAsync(FeedQuerySearch querySearch)
        {
            var query = _unitOfWork.FeedRepository.FindAll();

            if (!string.IsNullOrEmpty(querySearch.SearchKey))
            {
                query = query.Where(x => x.Content.Contains(querySearch.SearchKey));
            }

            if (!string.IsNullOrEmpty(querySearch.UserSlug))
            {
                query = query.Where(x => x.UserSlug.Equals(querySearch.UserSlug));
            }

            var feeds = await query.OrderByDescending(x => x.CreatedAt).Skip(querySearch.GetSkip()).Take(querySearch.GetTake()).ToListAsync();

            var result = new PageResultDto<FeedDto>(await query.CountAsync(), GetTotalPage(await query.CountAsync(), querySearch.GetTake()), feeds.Select(x => x.ToDto()).ToList());

            foreach (var item in result.Items)
            {
                var user = await _unitOfWork.UserRepository
                                    .FindAll(x => x.Id.Equals(item.CreatedBy))
                                    .Include(x => x.UserDetail)
                                    .SingleOrDefaultAsync();

                item.FullName = $"{user?.UserDetail?.FirstName} {user?.UserDetail?.LastName}";
                item.AvatarUrl = user?.UserDetail?.Avatar ?? $"{ApiUrl}/assets/no_image.png";
                item.UserSlug = querySearch.UserSlug;
            }

            return result;
        }
    }
}
