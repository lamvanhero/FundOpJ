using Microsoft.AspNetCore.Http;
using OppJar.Web.Models;
using OppJar.Web.Proxies;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        private const string CREATE_COMMENT = "/api/comment";

        public CommentService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        public async Task<HttpResponseMessage> CreatePost(CreateEditCommentViewModel vm)
        {
            return await _oppJarProxy.PostAsJsonAsync(CREATE_COMMENT, vm);
        }
    }
}
