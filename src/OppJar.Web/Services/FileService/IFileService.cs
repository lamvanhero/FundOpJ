using Microsoft.AspNetCore.Http;
using OppJar.Common.Enum;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public interface IFileService
    {
        Task<HttpResponseMessage> UploadFileAsync(IFormFile file, string id, UsingForType type = UsingForType.Avatar);

        Task<HttpResponseMessage> UploadFilesAsync(IFormFileCollection files, UsingForType type = UsingForType.Avatar);
    }
}
