using CSharpEmail;
using CSharpEmail.Interfaces;
using CSharpEmail.Models;
using JwtTokenServer.Models;
using JwtTokenServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OppJar.AutoMapper;
using OppJar.Common.Helpers;
using OppJar.Common.TokenSerializer;
using OppJar.Core.EmailTemplates;
using OppJar.Core.Exceptions;
using OppJar.Core.Settings;
using OppJar.Domain;
using OppJar.Common.Enum;
using OppJar.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OppJar.Core.Background.Queues;
using Microsoft.Extensions.Configuration;

namespace OppJar.Core.Services
{
    public class AccountManager : ServiceBase, IAccountService, IAccountManager
    {
        private const string TokenProviderResetPassword = "ResetPassword";
        private const string TokenProviderActiveAccount = "ActivateAccount";


        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IMemoryService _memoryService;

        public AccountManager(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher,
            ITokenProvider tokenProvider, IEmailSender emailSender,
            IBackgroundTaskQueue queue, IMemoryService memoryService, IConfiguration configuration) : base(unitOfWork, configuration)
        {
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _queue = queue;
            _memoryService = memoryService;
        }

        #region PRIVATE
        private async Task<User> CheckDuplicationUserAsync(string username)
        {
            return await _unitOfWork.UserRepository.FindAll(x => x.UserName.Equals(username)).SingleOrDefaultAsync();
        }

        private async Task<UserDto> CreateUserAsync(RegisterDto dto, string roleName)
        {
            var user = _unitOfWork.UserRepository.AddUser(roleName, dto);

            var userDetail = new UserDetail
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                SSN = dto.SSN,
                DOB = dto.DOB
            };

            userDetail.Avatar = $"{ApiUrl}{userDetail.Avatar}";

            _unitOfWork.UserDetailRepository.Add(userDetail);

            var userAddress = new Address
            {
                UserId = user.Id,
                PrimaryAddress = dto.PrimaryAddress,
                SecondAddress = dto.SecondAddress,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode
            };

            _unitOfWork.AddressRepository.Add(userAddress);

            await _unitOfWork.CommitAsync();

            return user.ToDto();
        }

        private async Task<UserDto> CreateUserAsync(ChildInfoDto dto, Func<User, Task> commit)
        {
            var parentInfo = await _unitOfWork.UserRepository.FindAll(x => x.Id.Equals(UserId))
                                        .Include(x => x.Address).SingleOrDefaultAsync();

            var user = _unitOfWork.UserRepository.AddChild(dto);

            var userDetail = new UserDetail
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DOB = dto.DOB,
                School = dto.School
            };

            userDetail.Avatar = $"{ApiUrl}{userDetail.Avatar}";

            _unitOfWork.UserDetailRepository.Add(userDetail);

            var userAddress = new Address
            {
                UserId = user.Id,
                PrimaryAddress = parentInfo.Address.PrimaryAddress,
                SecondAddress = parentInfo.Address.SecondAddress,
                City = parentInfo.Address.City,
                State = parentInfo.Address.State,
                ZipCode = parentInfo.Address.ZipCode
            };

            _unitOfWork.AddressRepository.Add(userAddress);

            var userEvent = new Event(user.Id);

            _unitOfWork.EventRepository.Add(userEvent);

            await _unitOfWork.CommitAsync(async () => await commit(user));

            return user.ToDto();
        }

        private async Task<bool> VerifyUserTokenActiveAccount(User user, string token)
        {
            return await VerifyUserTokenAsync(user, TokenProviderActiveAccount, token);
        }

        private async Task<bool> VerifyUserTokenResetPassword(User user, string token)
        {
            return await VerifyUserTokenAsync(user, TokenProviderResetPassword, token);
        }
        #endregion

        public async Task<object> ActivateAccountAsync(string email, string token)
        {
            var user = await _unitOfWork.UserRepository.FindAll(x => x.Email.Equals(email)).SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException($"Not found user with email {email}");

            var tokenValid = await VerifyUserTokenActiveAccount(user, HttpUtility.UrlDecode(token).Replace(" ", "+"));

            if (!tokenValid) throw new BadRequestException("Token invalid.");

            user.Status = UserStatus.Activate;

            await _unitOfWork.CommitAsync();

            return new { userId = user.Id, email = user.Email };
        }

        public async Task<UserDto> CreateAsync(RegisterDto dto)
        {
            if (await CheckDuplicationUserAsync(dto.Email) != null)
                throw new BadRequestException($"This user already exists.");

            var roleName = dto.UserType == UserType.Parent ? UserRoles.Parent : UserRoles.Giver;

            var user = await CreateUserAsync(dto, roleName);

            return user;
        }

        public async Task<UserDto> CreateAsync(BaseRegisterDto dto)
        {
            if (await CheckDuplicationUserAsync(dto.Email) != null)
                throw new BadRequestException($"This user already exists.");

            var user = await CreateUserAsync(new RegisterDto
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password
            }, UserRoles.Administrator);

            return user;
        }

        public async Task<UserDto> CreateAsync(ChildInfoDto dto)
        {
            var child = await CreateUserAsync(dto, async (entity) =>
            {
                var relationShip = new UserRelationship { UserId1 = UserId, User1Level = RelationshipLevel.Father, UserId2 = entity.Id, User2Level = RelationshipLevel.Son };

                _unitOfWork.UserRelationshipRepository.Add(relationShip);

                await _unitOfWork.CommitAsync();
            });

            return child;
        }

        public async Task<bool> ForgotPasswordAsync(string email, string password, string token)
        {
            var user = await _unitOfWork.UserRepository.FindAll(x => x.Email.Equals(email)).SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException($"Not found user with email {email}");

            var tokenValid = await VerifyUserTokenResetPassword(user, HttpUtility.UrlDecode(token));

            if (!tokenValid) throw new BadRequestException("Token invalid.");

            user.Password = _passwordHasher.HashPassword(user, password);

            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task SendMailActivateAccountAsync(string email, string redirectUrl)
        {
            var user = await _unitOfWork.UserRepository
                .FindAll(x => x.Email.Equals(email))
                .Include(x => x.UserDetail)
                .SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException($"Not found user with email {email}");

            var token = await GenerateUserTokenAsync(user, TokenProviderActiveAccount);

            var url = $"{redirectUrl}?email={email}&token={HttpUtility.UrlEncode(token)}";

            await _emailSender.SendEmailAsync(new Message
            {
                Subject = "Activate Your Account",
                Emails = new string[] { email },
                Body = HtmlParser.GenerateBody(new ActivateAccountTmpl(user.UserDetail.FirstName, user.UserDetail.LastName, url))
            });
        }

        public async Task SendMailForgotPasswordAsync(string email, string redirectUrl)
        {
            var user = await _unitOfWork.UserRepository
                .FindAll(x => x.Email.Equals(email))
                .Include(x => x.UserDetail)
                .SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException($"Not found user with email {email}");

            var token = await GenerateUserTokenAsync(user, TokenProviderResetPassword);

            var url = $"{redirectUrl}?email={email}&token={HttpUtility.UrlEncode(token)}";

            await _emailSender.SendEmailAsync(new Message
            {
                Subject = $"Reset your password for {user.UserDetail.FirstName} {user.UserDetail.LastName}",
                Emails = new string[] { email },
                Body = HtmlParser.GenerateBody(new ForgotPasswordTmpl(user.UserDetail.FirstName, user.UserDetail.LastName, url))
            });
        }

        public async Task SendMailConfirmParentAsync(string email, string redirectUrl, string firstName, string lastName, User user)
        {
            var token = await GenerateUserTokenAsync(user, TokenProviderActiveAccount);

            var url = $"{redirectUrl}?email={user.Email}&token={HttpUtility.UrlEncode(token)}";

            await _emailSender.SendEmailAsync(new Message
            {
                Subject = $"Confirm parent",
                Emails = new string[] { email },
                Body = HtmlParser.GenerateBody(new ConfirmParentTmpl(firstName, lastName, url))
            });
        }

        public async Task<AccountResult> VerifyAccountAsync(string username, string password, TokenRequest tokenRequest)
        {
            var user = await _unitOfWork.UserRepository
                            .FindAll(x => x.UserName.Equals(username))
                            .Include(x => x.UserDetail)
                            .SingleOrDefaultAsync();

            if (user == null) return new AccountResult { Error = "Invalid user." };

            if (user.Status == UserStatus.Deactivate) return new AccountResult { Error = "An e-mail was sent to you to activate your account." };

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
            {
                return new AccountResult { Error = "Invalid password." };
            }

            if (!tokenRequest.Claims.Any(x => x.Type.Equals(ClaimKeyHelper.USER_ID)))
            {
                tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.USER_ID, user.Id));
            }

            var userRole = await _unitOfWork.UserRoleRepository
                                .FindAll(x => x.UserId.Equals(user.Id))
                                .Include(x => x.Role)
                                .FirstOrDefaultAsync();

            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.ROLE, userRole.Role.Name));

            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.CUSTOMER_ID, user.UserDetail?.CustomerId ?? string.Empty));

            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.EMAIL, user.Email));

            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.NAME, $"{user.UserDetail?.FirstName} {user.UserDetail?.LastName}"));

            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.AVATAR, user.UserDetail?.Avatar));
            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.FIRST_NAME, user.UserDetail?.FirstName));
            tokenRequest.Claims.Add(new CustomClaim(ClaimKeyHelper.LAST_NAME, user.UserDetail?.LastName));

            tokenRequest.Responses.Add("displayName", $"{user?.UserDetail?.FirstName} {user?.UserDetail?.LastName}");

            tokenRequest.Responses.Add("userId", user.Id);

            tokenRequest.Responses.Add("role", userRole.Role.Name);

            return new AccountResult(tokenRequest);
        }

        public async Task<string> GenerateUserTokenAsync(User user, string purpose)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.CommitAsync();

            return _tokenProvider.GenerateToken(purpose, user);
        }

        public async Task<bool> VerifyUserTokenAsync(User user, string purpose, string token)
        {
            var result = _tokenProvider.ValidateToken(purpose, user, token);

            user.SecurityStamp = Guid.NewGuid().ToString();

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.CommitAsync();

            return result.Validated;
        }

        public async Task<UserDto> EditAsync(string userId, EditProfileDto dto)
        {
            var query = _unitOfWork.UserRepository
                .FindAll(x => x.Id.Equals(userId));

            if (query == null) throw new NotFoundException("Not found user.");

            var user = await query
                .Include(x => x.UserDetail)
                .Include(x => x.Address)
                .SingleOrDefaultAsync();

            var detail = dto.ToDetailEntity(user.UserDetail);

            _unitOfWork.UserDetailRepository.Update(detail);

            if (dto.UserType == UserType.Parent)
            {
                var address = dto.ToAddressEntity(user.Address);

                _unitOfWork.AddressRepository.Update(address);
            }

            await _unitOfWork.CommitAsync();

            return user.ToDto();
        }

        public async Task<UserDetailDto> GetUserDetailByUserIdAsync(string id)
        {
            var user = await _unitOfWork.UserRepository
                .FindAll(x => x.Id.Equals(id))
                .Include(x => x.UserDetail)
                .Include(x => x.Address)
                .Include(x => x.UserRole)
                    .ThenInclude(y => y.Role)
                .SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException("Not found user.");

            return user.ToDetailDto();
        }

        public async Task<UserDetailDto> GetUserDetailBySlugAsync(string slug)
        {
            var user = await _unitOfWork.UserRepository
                .FindAll(x => x.Slug.Equals(slug))
                .Include(x => x.UserDetail)
                .Include(x => x.Address)
                .Include(x => x.UserRole)
                    .ThenInclude(y => y.Role)
                .SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException("Not found user.");

            return user.ToDetailDto();
        }

        public async Task<PageResultDto<UserDetailDto>> SearchAsync(AccountQuerySearch param)
        {
            var query = _unitOfWork.UserRepository.FindAll()
                            .Include(x => x.UserDetail)
                            .Include(x => x.Address)
                            .Include(x => x.UserRole)
                                .ThenInclude(x => x.Role)
                            .AsQueryable();

            switch (param.UserType)
            {
                case UserType.Administrator:
                    {
                        query = query.Where(x => x.UserRole.Role.Name.Equals(UserRoles.Administrator));
                        break;
                    }
                case UserType.Child:
                    {
                        query = query.Where(x => x.UserRole.Role.Name.Equals(UserRoles.Child));
                        break;
                    }
                case UserType.Parent:
                    {
                        query = query.Where(x => x.UserRole.Role.Name.Equals(UserRoles.Parent));
                        break;
                    }
                case UserType.Giver:
                    {
                        query = query.Where(x => x.UserRole.Role.Name.Equals(UserRoles.Giver));
                        break;
                    }
                default:
                    {
                        query = query.Where(x => x.UserRole.Role.Name.Equals(UserRoles.Parent) || x.UserRole.Role.Name.Equals(UserRoles.Giver));
                        break;
                    }
            }

            if (param.IsOwner)
            {
                query = query.Where(x => x.CreatedBy.Equals(UserId));
            }

            if (!string.IsNullOrEmpty(param.SearchKey))
            {
                query = query.Where(x => EF.Functions.Like(x.UserDetail.FirstName, $"%{param.SearchKey}%")
                || EF.Functions.Like(x.UserDetail.LastName, $"%{param.SearchKey}%"));
            }

            var result = await query.OrderByDescending(x => x.CreatedAt).Skip(param.GetSkip()).Take(param.GetTake()).ToListAsync();

            return new PageResultDto<UserDetailDto>(await query.CountAsync(), GetTotalPage(await query.CountAsync(), param.GetTake()), result.Select(x => x.ToDetailDto()));
        }

        public async Task<UserDto> ActiveDeactiveUserAsync(string id, UserStatus userStatus = UserStatus.Activate)
        {
            var user = await _unitOfWork.UserRepository.FindAll(x => x.Id.Equals(id)).SingleOrDefaultAsync();

            if (user == null) throw new NotFoundException("User not found.");

            user.Status = userStatus;

            await _unitOfWork.CommitAsync();

            return user.ToDto();
        }

        public async Task<DonationResultDto> DonateAsync(string id, DonationDto dto)
        {
            var user = await _unitOfWork.UserRepository.FindAll(x => x.Id.Equals(id))
                .Include(x => x.UserDetail)
                .FirstOrDefaultAsync();

            if (user == null) throw new NotFoundException("User not found.");

            user.TotalBalance += dto.Amount;

            await _unitOfWork.CommitAsync();

            var parent = await _unitOfWork.UserRepository.FindAll(x => x.Id.Equals(dto.CreatedBy)).Include(x => x.UserDetail).SingleOrDefaultAsync();

            _queue.QueueBackgroundWorkItem(async (serviceFactory) =>
            {
                await _emailSender.SendEmailAsync(new Message
                {
                    Subject = "Donation inform",
                    Emails = new string[] { dto.Email },
                    Body = HtmlParser.GenerateBody(new DonationTmpl(dto.SenderName, $"{user.UserDetail.FirstName} {user.UserDetail.LastName}", dto.Amount.ToString()))
                });

                await _emailSender.SendEmailAsync(new Message
                {
                    Subject = "Donation inform",
                    Emails = new string[] { parent.Email },
                    Body = HtmlParser.GenerateBody(new DonationMailToParentTmpl($"{parent.UserDetail.FirstName} {parent.UserDetail.LastName}", dto.SenderName, $"{user.UserDetail.FirstName} {user.UserDetail.LastName}", dto.Amount.ToString()))
                });
            });

            await _memoryService.CreateFeedAsync(new CreateFeedDto
            {
                Content = $"{dto.SenderName} just invested in {user.UserDetail.FirstName}'s future!",
                FeedType = FeedType.Donation,
                UserSlug = user.Slug,
                FileUrl = $"{ApiUrl}/assets/jar.png"
            });

            var donationResultDto = new DonationResultDto
            {
                IsSuccess = true,
                CurrentAmount = user.TotalBalance
            };

            return donationResultDto;
        }

        public async Task<bool> UpdateCustomerIdAsync(string id, string customerId)
        {
            var user = await _unitOfWork.UserDetailRepository.FindAll(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            if (user == null) throw new NotFoundException("User not found.");

            user.CustomerId = customerId;

            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
