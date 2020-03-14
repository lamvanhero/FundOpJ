using Microsoft.AspNetCore.Mvc;
using OppJar.Core.Services;
using OppJar.Dto;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    public class CommentController : BaseApi
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] CommentQuerySearch querySearch)
        {
            return Ok(await _commentService.SearchAsync(querySearch));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEditCommentDto dto)
        {
            return Ok(await _commentService.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CreateEditCommentDto dto)
        {
            return Ok(await _commentService.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _commentService.DeleteAsync(id);

            return Success();
        }
    }
}
