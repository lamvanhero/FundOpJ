using Microsoft.AspNetCore.Http;
using OppJar.Common.Helpers;
using OppJar.Web.Proxies;
using System.Security.Claims;

namespace OppJar.Web.Services
{
    public abstract class ServiceBase
    {
        protected readonly OppJarProxy _oppJarProxy;

        protected readonly HttpContext _httpContext;

        protected string Token => _httpContext.Request.Cookies["access_token"];

        protected ClaimsPrincipal CurrentUser
        {
            get
            {
                return _httpContext.User ?? null;
            }
        }

        protected string UserId => CurrentUser.FindFirstValue(ClaimKeyHelper.USER_ID);

        public ServiceBase(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy)
        {
            _httpContext = httpContextAccessor.HttpContext;

            _oppJarProxy = oppJarProxy;

            if (!string.IsNullOrEmpty(Token))
            {
                _oppJarProxy.SetToken(Token);
            }
        }
    }
}
