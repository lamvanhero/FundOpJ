using System.Collections.Generic;
using System.Threading.Tasks;
using JwtTokenServer.Proxies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OppJar.Core.Services;
using OppJar.Common.Enum;
using OppJar.Dto;
using OppJar.Core.Services.Model;
using OppJar.Core.Exceptions;

namespace OppJar.WebApi.Controllers
{
    public class AccountController : BaseApi
    {
        private readonly IAccountService _accountService;
        private readonly ITruliooService _truliooService;
        private readonly OAuthClient _oAuthClient;

        public AccountController(OAuthClient oAuthClient, IAccountService accountService,
            ITruliooService truliooService)
        {
            _accountService = accountService;

            _oAuthClient = oAuthClient;

            _truliooService = truliooService;
        }

        #region ANONYMOUS
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            //if (dto.Type == UserType.Parent)
            //{
            //    var verifyResult = await _truliooService.VerifyAsync(new VerifyRequest
            //    {
            //        DataFields = new DataFields
            //        {
            //            PersonInfo = new PersonInfo
            //            {
            //                FirstGivenName = dto.FirstName,
            //                FirstSurName = dto.LastName
            //            },
            //            Location = new Location
            //            {
            //                City = dto.City,
            //                StateProvinceCode = dto.State,
            //                PostalCode = dto.ZipCode
            //            },
            //            NationalIds = new NationalId[]
            //            {
            //                new NationalId
            //                {
            //                    Number = dto.SSN,
            //                    Type = "SocialService"
            //                }
            //            }
            //        }
            //    });

            //    if (verifyResult.Record.RecordStatus.Equals("nomatch")) throw new BadRequestException("You're not verified.");
            //}

            var result = await _accountService.CreateAsync(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _oAuthClient.EnsureApiTokenAsync(dto.Username, dto.Password);

            if (response.Success) return Ok(response.Result);

            return BadRequest(response);
        }

        [HttpPost("send-mail-activate-account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> SendMailActivateAccountAsync([FromBody] ResetPasswordDto dto)
        {
            await _accountService.SendMailActivateAccountAsync(dto.Email, dto.RedirectUrl);

            return Success();
        }

        [HttpPost("activate-account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> ActivateAccountAssync([FromBody] ActivateAccountDto dto)
        {
            return Ok(await _accountService.ActivateAccountAsync(dto.Email, dto.Token));
        }

        [HttpPost("send-mail-forgot-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> SendMailForgotPassword([FromBody] ResetPasswordDto dto)
        {
            await _accountService.SendMailForgotPasswordAsync(dto.Email, dto.RedirectUrl);

            return Success();
        }

        [HttpPost("forgot-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _accountService.ForgotPasswordAsync(dto.Email, dto.Password, dto.Token);

            return Success();
        }

        [HttpPut("{id}/donate")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> Donate(string id, DonationDto dto)
        {
            var result = await _accountService.DonateAsync(id, dto);

            return Ok(result);
        }
        #endregion

        #region PARENT
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromBody] EditProfileDto dto, string id)
        {
            //if (dto.UserType == UserType.Parent)
            //{
            //    var verifyResult = await _truliooService.VerifyAsync(new VerifyRequest
            //    {
            //        DataFields = new DataFields
            //        {
            //            Location = new Location
            //            {
            //                City = dto.City,
            //                StateProvinceCode = dto.State,
            //                PostalCode = dto.ZipCode
            //            }
            //        }
            //    });

            //    if (verifyResult.Record.RecordStatus.Equals("nomatch")) throw new BadRequestException("You're not verified.");
            //}

            var result = await _accountService.EditAsync(id, dto);

            return Ok(result);
        }

        [Authorize(Roles = "Parent, Giver")]
        [HttpPut("{id}/customer/{customerId}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateCustomerId(string id, string customerId)
        {
            var result = await _accountService.UpdateCustomerIdAsync(id, customerId);

            if (result) return Success();

            return BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserDetailById(string id)
        {
            return Ok(await _accountService.GetUserDetailByUserIdAsync(id));
        }

        [HttpGet("slug/{slug}")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserDetailBySlug(string slug)
        {
            return Ok(await _accountService.GetUserDetailBySlugAsync(slug));
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IActionResult> Search([FromQuery] AccountQuerySearch param)
        {
            return Ok(await _accountService.SearchAsync(param));
        }

        [HttpPut("{id}/status/{status}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateStatus(string id, UserStatus status)
        {
            var result = await _accountService.ActiveDeactiveUserAsync(id, status);

            return Ok(result);
        }

        [HttpGet("current-user-profile")]
        [ProducesResponseType(typeof(CurrentUserProfileDto), 200)]
        public IActionResult CurrentUserProfile()
        {
            return Ok(new CurrentUserProfileDto(RoleName, Email, UserId, CustomerId, DisplayName, Avatar, FirstName, LastName));
        }

        [Authorize(Roles = "Parent")]
        [HttpGet("children")]
        public async Task<IActionResult> GetChildren()
        {
            var result = await _accountService.SearchAsync(new AccountQuerySearch
            {
                IsOwner = true,
                UserType = UserType.Child
            });

            return Ok(result);
        }
        #endregion

        #region CHILD
        [Authorize(Roles = "Parent")]
        [HttpPost("child")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChild([FromBody] ChildInfoDto dto)
        {
            var result = await _accountService.CreateAsync(dto);

            return Ok(result);
        }
        #endregion

        #region ADMIN
        [Authorize(Roles = "SuperAdministrator")]
        [HttpPost("admin")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAdmin([FromBody] BaseRegisterDto dto)
        {
            var result = await _accountService.CreateAsync(dto);

            return Ok(result);
        }
        #endregion
    }
}
