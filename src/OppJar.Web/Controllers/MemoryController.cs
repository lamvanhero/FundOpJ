using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Helpers;
using OppJar.Dto;
using OppJar.Common.Enum;
using OppJar.Web.Models;
using OppJar.Web.Services;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OppJar.Web.Helpers;

namespace OppJar.Web.Controllers
{
    [Authorize(Roles = "Parent")]
    public class MemoryController : BaseController
    {
        private readonly IMemoryService _memoryService;
        private readonly ICommentService _commentService;
        private readonly IAccountService _accountService;

        public MemoryController(IMapper mapper, IConfiguration configuration,
            IMemoryService memoryService, ICommentService commentService, IAccountService accountService) : base(mapper, configuration)
        {
            _memoryService = memoryService;
            _commentService = commentService;
            _accountService = accountService;
        }

        [HttpGet("user/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var response = await _accountService.GetUserDetailBySlugAsync(slug);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var model = json.JsonToObj<ProfileViewModel>();

                var children = await _accountService.GetChildrenAsync();

                if (!children.Items.Any(x => x.CreatedBy.Equals(UserId)))
                {
                    return Forbid();
                }

                var feedResponse = await _memoryService.SearchAsync(new FeedQuerySearch
                {
                    UserSlug = slug
                });

                if (feedResponse.IsSuccessStatusCode)
                {
                    var feedJson = await feedResponse.Content.ReadAsStringAsync();

                    var feeds = feedJson.JsonToObj<PageResultDto<MemoryViewModel>>();

                    foreach (var feed in feeds.Items)
                    {
                        feed.Preview = feed.Preview.BuildPreview();
                    }

                    model.TotalPage = feeds.TotalPage;
                    model.Memories.AddRange(feeds.Items);
                }

                return View(model);
            }

            return NotFound();
        }

        [HttpGet("user/{slug}/feeds/{page}")]
        public async Task<IActionResult> LoadMoreFeeds(string slug, int page)
        {
            var response = await _memoryService.SearchAsync(new FeedQuerySearch
            {
                UserSlug = slug,
                Page = page
            });

            if (response.IsSuccessStatusCode)
            {
                var feedJson = await response.Content.ReadAsStringAsync();

                var feeds = feedJson.JsonToObj<PageResultDto<MemoryViewModel>>();

                foreach (var feed in feeds.Items)
                {
                    feed.Preview = feed.Preview.BuildPreview();
                }

                return PartialView("~/Views/Memory/_MemoryUpdate.cshtml", feeds.Items);
            }

            return Redirect("/Error/500");
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CreateEditCommentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _commentService.CreatePost(vm);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var model = json.JsonToObj<CommentViewModel>();

                    return PartialView("~/Views/Memory/_MemoryComment.cshtml", model);
                }

                return Json(response.GetErrors());
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{slug}/posts/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> MemoryDetail(string slug, string id)
        {
            var response = await _memoryService.GetByIdAsync(id, slug);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound) return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();

            var feed = json.JsonToObj<MemoryViewModel>();

            feed.Preview = feed.Preview.BuildPreview();

            if (feed.Privacy == Privacy.Private)
            {
                if (!feed.CreatedBy.Equals(UserId)) return Forbid();
            }

            return View("~/Views/Memory/MemoryDetail.cshtml", feed);
        }
    }
}
