using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public interface IFileService
    {
        Task<FileStreamResult> GetFileAsync(string fileName);

        Task<List<string>> UploadMultiFileAsync(IFormFileCollection files, UsingForType type = UsingForType.Avatar);

        Task<string> UploadFileAsync(IFormFile file, string userId = null, UsingForType type = UsingForType.Avatar);
    }
}
