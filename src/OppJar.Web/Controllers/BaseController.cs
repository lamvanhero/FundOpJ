using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Helpers;
using System.Security.Claims;

namespace OppJar.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BaseController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        protected string UserId
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimKeyHelper.USER_ID).Value;
            }
        }

        protected string DisplayName
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            }
        }

        protected string Email
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimKeyHelper.EMAIL).Value;
            }
        }

        protected string Role
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;
            }
        }

        protected string FirstName
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimKeyHelper.FIRST_NAME).Value;
            }
        }

        protected string LastName
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated) return null;

                return HttpContext.User.FindFirst(ClaimKeyHelper.LAST_NAME).Value;
            }
        }

        protected string BaseUrl
        {
            get
            {
                return $"{Request.Scheme}://{Request.Host}";
            }
        }

        protected string ApiUrl
        {
            get
            {
                return _configuration.GetValue<string>("OppJarProxyUrl");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.AvatarUrl = User.FindFirstValue("Avatar");

            ViewBag.DisplayName = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            ViewBag.Email = Email;

            ViewBag.UserId = UserId;

            ViewBag.ApiUrl = ApiUrl;

            ViewBag.LinkPreviewKey = _configuration.GetValue<string>("LinkPreviewKey");

            ViewBag.PublishableKey = _configuration.GetSection("Stripe").GetValue<string>("PublishableKey");

            base.OnActionExecuted(context);
        }

        protected TDto VmToDto<TViewModel, TDto>(TViewModel vm)
        {
            return _mapper.Map<TDto>(vm);
        }

        protected TViewModel DtoToVm<TViewModel, TDto>(TDto vm)
        {
            return _mapper.Map<TViewModel>(vm);
        }
    }
}
