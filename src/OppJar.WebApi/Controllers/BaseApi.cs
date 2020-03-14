using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Helpers;

namespace OppJar.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class BaseApi : ControllerBase
    {
        protected string DisplayName => User.GetValue(ClaimKeyHelper.NAME);
        protected string RoleName => User.GetValue(ClaimKeyHelper.ROLE);
        protected string UserId => User.GetValue(ClaimKeyHelper.USER_ID);
        protected string Email => User.GetValue(ClaimKeyHelper.EMAIL);
        protected string Avatar => User.GetValue(ClaimKeyHelper.AVATAR);
        protected string CustomerId => User.GetValue(ClaimKeyHelper.CUSTOMER_ID);
        protected string FirstName => User.GetValue(ClaimKeyHelper.FIRST_NAME);
        protected string LastName => User.GetValue(ClaimKeyHelper.LAST_NAME);
        protected OkObjectResult Success()
        {
            return Ok(new { success = true });
        }
    }
}
