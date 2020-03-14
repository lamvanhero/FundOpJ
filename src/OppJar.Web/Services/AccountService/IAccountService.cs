using OppJar.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Enum;
using OppJar.Dto;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace OppJar.Web.Services
{
    public interface IAccountService
    {
        Task<JObject> SignInAsync(LoginDto dto);

        Task<bool> SignOutAsync();

        Task<HttpResponseMessage> RegisterAsync(RegisterDto dto);

        Task<CurrentUserProfileModel> GetCurrentUserAsync();

        Task<HttpResponseMessage> GetUserDetailBySlugAsync(string slug);

        Task<HttpResponseMessage> GetUserDetailByIdAsync(string id);

        Task<HttpResponseMessage> UpdateUserDetailByIdAsync(EditProfileDto dto, string id);

        Task<HttpResponseMessage> ActivateAccountAssync(ActivateAccountDto dto);

        Task SendMailActivateAccountAsync(ResetPasswordDto dto);

        Task<HttpResponseMessage> ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<HttpResponseMessage> SendMailForgotPasswordAsync(ResetPasswordDto dto);

        Task<PageResultDto<UserDetailDto>> SearchAsync(AccountQuerySearch accountQuerySearch);

        Task<HttpResponseMessage> ChangeStatusAsync(UserStatus dto, string id);

        Task<HttpResponseMessage> CreateAdminAsync(BaseRegisterDto dto);

        Task<FileStreamResult> GetImageAsync(string fileName);

        Task<PageResultDto<UserDetailDto>> GetChildrenAsync();

        Task<HttpResponseMessage> DonateAsync(string id, DonationDto dto);

        Task<HttpResponseMessage> CreateChildAsync(ChildInfoDto dto);
    }
}
