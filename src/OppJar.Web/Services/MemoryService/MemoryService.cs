using Microsoft.AspNetCore.Http;
using OppJar.Common.Helpers;
using OppJar.Dto;
using OppJar.Web.Proxies;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public class MemoryService : ServiceBase, IMemoryService
    {
        private const string SEARCH = "/api/memory/search?{0}";
        private const string GET_BY_ID = "/api/memory/{0}/{1}";

        public MemoryService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        public async Task<HttpResponseMessage> GetByIdAsync(string id, string slug)
        {
            return await _oppJarProxy.GetAsync(string.Format(GET_BY_ID, slug, id));
        }

        public async Task<HttpResponseMessage> SearchAsync(FeedQuerySearch querySearch)
        {
            return await _oppJarProxy.GetAsync(string.Format(SEARCH, querySearch.GetQueryString()));
        }
    }
}
