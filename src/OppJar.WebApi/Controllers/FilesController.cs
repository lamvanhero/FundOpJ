using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OppJar.Core.Services;
using OppJar.Common.Enum;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    public class FilesController : BaseApi
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        [Route("{fileName}")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<FileStreamResult> GetFile(string fileName)
        {
            return await _fileService.GetFileAsync(fileName);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile file, string userId = null, UsingForType type = UsingForType.Avatar)
        {
            if (file == null) return BadRequest("No file to upload.");

            return Ok(await _fileService.UploadFileAsync(file, userId, type));
        }

        [HttpPost("multiple")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files, UsingForType type = UsingForType.Avatar)
        {
            if (files.Count > 0)
            {
                return Ok(await _fileService.UploadMultiFileAsync(files, type));
            }
            else
            {
                return BadRequest("No files to upload.");
            }
        }
    }
}
