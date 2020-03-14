using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using OppJar.Common.Helpers;
using OppJar.Web.Models;
using OppJar.Web.Proxies;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Enum;
using OppJar.Dto;
using System.Net.Http;

namespace OppJar.Web.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        public AccountService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        public async Task<JObject> SignInAsync(LoginDto dto)
        {
            var result = await _oppJarProxy.LoginAsync(dto);

            if (result.TryGetValue("success", out JToken success))
            {
                if (success.Value<bool>() == false)
                {
                    return result;
                }
            }

            string access_token = (string)result.Property("accessToken").Value;

            string refresh_token = (string)result.Property("refreshToken").Value;

            string userId = (string)result.Property("userId").Value;

            string role = (string)result.Property("role").Value;

            int expires = (int)result.Property("expires").Value;

            if (!string.IsNullOrEmpty(access_token))
            {
                _oppJarProxy.SetToken(access_token);
            }

            var profile = await _oppJarProxy.CurrentUserProfileAsync();

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimKeyHelper.USER_ID, userId));

            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, profile.DisplayName));

            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, profile.RoleName));

            identity.AddClaim(new Claim(ClaimKeyHelper.EMAIL, profile.Email));

            identity.AddClaim(new Claim(ClaimKeyHelper.AVATAR, profile.Avatar));

            identity.AddClaim(new Claim(ClaimKeyHelper.FIRST_NAME, profile.FirstName));

            identity.AddClaim(new Claim(ClaimKeyHelper.LAST_NAME, profile.LastName));

            _httpContext.Response.Cookies.Append($"access_token", access_token);

            _httpContext.Response.Cookies.Append($"refresh_token", refresh_token);

            _httpContext.Response.Cookies.Append($"expires", expires.ToString());

            _httpContext.Response.Cookies.Append($"token_key",
                CipherHelper.Encrypt(dto.ObjToJson()));

            _httpContext.Response.Cookies.Append($"role", role);

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(expires)
                });

            return result;
        }

        public async Task<bool> SignOutAsync()
        {
            _httpContext.Response.Cookies.Delete($"access_token");
            _httpContext.Response.Cookies.Delete($"token_key");
            _httpContext.Response.Cookies.Delete($"refresh_token");
            _httpContext.Response.Cookies.Delete($"role");
            _httpContext.Response.Cookies.Delete($"expires");

            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return true;
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterDto vm)
        {
            return await _oppJarProxy.RegisterAsync(vm);
        }

        public async Task<CurrentUserProfileModel> GetCurrentUserAsync()
        {
            var result = await _oppJarProxy.CurrentUserProfileAsync();

            return result;
        }

        public async Task<HttpResponseMessage> GetUserDetailBySlugAsync(string slug)
        {
            return await _oppJarProxy.GetUserDetailBySlugAsync(slug);
        }

        public async Task<HttpResponseMessage> GetUserDetailByIdAsync(string id)
        {
            return await _oppJarProxy.GetUserDetailByIdAsync(id);
        }

        public async Task<HttpResponseMessage> UpdateUserDetailByIdAsync(EditProfileDto dto, string id)
        {
            var result = await _oppJarProxy.UpdateUserDetailByIdAsync(dto, id);

            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();

                var user = json.JsonToObj<UserDto>();

                if (user.Id.Equals(UserId))
                {
                    var claimsIdentity = _httpContext.User.Identity as ClaimsIdentity;

                    var avatarClaim = claimsIdentity.FindFirst("Avatar");

                    if (avatarClaim != null) claimsIdentity.RemoveClaim(avatarClaim);

                    claimsIdentity.AddClaim(new Claim("Avatar", dto.Avatar));

                    await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = false,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(int.Parse(_httpContext.Request.Cookies["expires"]))
                        });
                }
            }

            return result;
        }

        public async Task<HttpResponseMessage> ActivateAccountAssync(ActivateAccountDto dto)
        {
            return await _oppJarProxy.ActivateAccountAssync(dto);
        }

        public async Task SendMailActivateAccountAsync(ResetPasswordDto dto)
        {
            await _oppJarProxy.SendMailActivateAccountAsync(dto);
        }

        public async Task<HttpResponseMessage> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            return await _oppJarProxy.ForgotPasswordAsync(dto);
        }

        public async Task<HttpResponseMessage> SendMailForgotPasswordAsync(ResetPasswordDto dto)
        {
            return await _oppJarProxy.SendMailForgotPasswordAsync(dto);
        }

        public async Task<PageResultDto<UserDetailDto>> SearchAsync(AccountQuerySearch accountQuerySearch)
        {
            return await _oppJarProxy.SearchAsync(accountQuerySearch);
        }

        public async Task<HttpResponseMessage> ChangeStatusAsync(UserStatus dto, string id)
        {
            return await _oppJarProxy.ChangeStatusAsync(dto, id);
        }

        public async Task<HttpResponseMessage> CreateAdminAsync(BaseRegisterDto dto)
        {
            return await _oppJarProxy.CreateAdminAsync(dto);
        }

        public async Task<FileStreamResult> GetImageAsync(string fileName)
        {
            var result = await _oppJarProxy.GetImageAsync(fileName);

            return result;
        }

        public async Task<PageResultDto<UserDetailDto>> GetChildrenAsync()
        {
            var response = await _oppJarProxy.GetChildrenAsync();

            return await response.Content.ReadAsAsync<PageResultDto<UserDetailDto>>();
        }

        public async Task<HttpResponseMessage> DonateAsync(string id, DonationDto dto)
        {
            return await _oppJarProxy.DonateAsync(id, dto);
        }

        public async Task<HttpResponseMessage> CreateChildAsync(ChildInfoDto dto)
        {
            return await _oppJarProxy.CreateChildAsync(dto);
        }
    }
}
