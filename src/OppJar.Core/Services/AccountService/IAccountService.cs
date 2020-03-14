using OppJar.Domain;
using OppJar.Common.Enum;
using OppJar.Dto;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public interface IAccountService
    {
        Task<UserDto> CreateAsync(RegisterDto dto);
        Task<UserDto> CreateAsync(BaseRegisterDto dto);
        Task<UserDto> CreateAsync(ChildInfoDto dto);
        Task<UserDto> EditAsync(string userId, EditProfileDto dto);
        Task<UserDetailDto> GetUserDetailByUserIdAsync(string id);
        Task<UserDetailDto> GetUserDetailBySlugAsync(string slug);
        Task<PageResultDto<UserDetailDto>> SearchAsync(AccountQuerySearch param);
        Task<UserDto> ActiveDeactiveUserAsync(string id, UserStatus userStatus = UserStatus.Activate);
        Task<string> GenerateUserTokenAsync(User user, string purpose);
        Task<bool> VerifyUserTokenAsync(User user, string purpose, string token);
        Task SendMailActivateAccountAsync(string email, string redirectUrl);
        Task<object> ActivateAccountAsync(string email, string token);
        Task SendMailForgotPasswordAsync(string email, string redirectUrl);
        Task<bool> ForgotPasswordAsync(string email, string password, string token);
        Task<DonationResultDto> DonateAsync(string id, DonationDto dto);
        Task<bool> UpdateCustomerIdAsync(string id, string customerId);
    }
}
