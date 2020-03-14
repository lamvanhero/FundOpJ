using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Enum;
using OppJar.Core.Services;
using OppJar.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    public class MemoryController : BaseApi
    {
        private readonly IMemoryService _feedService;
        private readonly ICommentService _commentService;

        public MemoryController(IMemoryService feedService, ICommentService commentService)
        {
            _feedService = feedService;
            _commentService = commentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FeedDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create([FromBody] CreateFeedDto dto)
        {
            var result = await _feedService.CreateFeedAsync(dto);

            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PageResultDto<FeedDto>), 200)]
        public async Task<IActionResult> Get([FromQuery] FeedQuerySearch querySearch)
        {
            var feeds = await _feedService.SearchAsync(querySearch);

            foreach (var feed in feeds.Items)
            {
                var comments = await _commentService.SearchAsync(new CommentQuerySearch { ReferId = feed.Id, Size = 5 });

                comments.Items.Where(x => x.CreatedBy.Equals(feed.CreatedBy)).ToList().ForEach(x => x.ProfilePictureUrl = feed.AvatarUrl);

                feed.Comments = comments;
            }

            return Ok(feeds);
        }

        [HttpGet("{slug}/{id}")]
        [ProducesResponseType(typeof(FeedDto), 200)]
        [ProducesResponseType(404)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id, string slug)
        {
            var feed = await _feedService.GetByIdAsync(id, slug);

            var comments = await _commentService.SearchAsync(new CommentQuerySearch { ReferId = feed.Id, Size = 5 });

            comments.Items.Where(x => x.CreatedBy.Equals(feed.CreatedBy)).ToList().ForEach(x => x.ProfilePictureUrl = feed.AvatarUrl);

            feed.Comments = comments;

            return Ok(feed);
        }

        [HttpPut("{id}/change-privacy/{privacy}")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ChangePrivacy(string id, Privacy privacy = Privacy.Private)
        {
            return Ok(await _feedService.ChangePrivacyByIdAsync(id, privacy));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            await _feedService.DeleteByIdAsync(id);

            await _commentService.DeleteByIdAsync(id);

            return Success();
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> Like(string id)
        {
            await _feedService.LikeAsync(id);

            return Success();
        }
    }
}
