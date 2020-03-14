using Microsoft.AspNetCore.Http;
using OppJar.Common.Enum;
using OppJar.Web.Proxies;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public class FileService : ServiceBase, IFileService
    {
        public FileService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        public async Task<HttpResponseMessage> UploadFileAsync(IFormFile file, string id, UsingForType type = UsingForType.Avatar)
        {
            return await _oppJarProxy.UploadFileAsync(file, id, type);
        }

        public async Task<HttpResponseMessage> UploadFilesAsync(IFormFileCollection files, UsingForType type = UsingForType.Avatar)
        {
            return await _oppJarProxy.UploadFilesAsync(files, type);
        }
    }
}
