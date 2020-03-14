using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OppJar.Common.Enum;
using OppJar.Common.Helpers;
using OppJar.Dto;
using OppJar.Web.Helpers;
using OppJar.Web.Models;
using OppJar.Web.Services;

namespace OppJar.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IEventService _eventService;
        private readonly IInfrastructorService _infrastructorService;
        private readonly IFileService _fileService;

        public AccountController(IMapper mapper, IConfiguration configuration,
            IAccountService accountService, IEventService eventService,
            IInfrastructorService infrastructorService, IFileService fileService) : base(mapper, configuration)
        {
            _accountService = accountService;
            _eventService = eventService;
            _infrastructorService = infrastructorService;
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel vm, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _accountService.SignInAsync(VmToDto<LoginViewModel, LoginDto>(vm));

                if (!result.TryGetValue("userId", out JToken userId))
                {
                    ModelState.AddModelError("Email", (string)result.Property("result").Value);

                    return View(vm);
                }

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                var role = (string)result.Property("role").Value;

                if (role.Equals("Administrator") || role.Equals("SuperAdministrator"))
                    return Redirect("/Admins");

                if (role.Equals("Giver")) return RedirectToAction("Index", "Give");

                return RedirectToAction("Dashboard", "Account");
            }

            return View(vm);
        }

        public async Task<IActionResult> SignOut()
        {
            await _accountService.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var states = await GetStates();

            ViewBag.States = new SelectList(states, "Id", "Name");

            return View(new RegisterViewModel { UserType = UserType.Parent, DOB = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var states = await GetStates();

            var stId = states.FirstOrDefault(x => x.Id.ToString().Equals(vm.State))?.Id;

            ViewBag.States = new SelectList(states, "Id", "Name");

            if (stId != null)
            {
                var cities = await GetCities(int.Parse(stId));
                ViewBag.Cities = new SelectList(cities, "Id", "Name");
            }

            if (ModelState.IsValid)
            {
                var model = VmToDto<RegisterViewModel, RegisterDto>(vm);

                model.SSN = model.SSN.Replace(" ", "");

                model.State = states.FirstOrDefault(x => x.Id.ToString().Equals(vm.State))?.Code;

                var response = await _accountService.RegisterAsync(model);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var dtoConfirm = new ResetPasswordDto()
                    {
                        Email = json.JsonToObj<UserDto>().Email,
                        RedirectUrl = $"{BaseUrl}/Account/ConfirmEmail"
                    };

                    await _accountService.SendMailActivateAccountAsync(dtoConfirm);

                    return RedirectToAction("RegisterConfirmation", "Account", dtoConfirm);
                }

                ModelState.AddModelError(string.Empty, response.GetErrors());

                return View(vm);
            }

            return View(vm);
        }

        public ActionResult RegisterConfirmation(ResetPasswordDto dto)
        {
            return View();
        }

        [HttpGet]
        public async Task ResendEmailActive(string email)
        {
            var dtoConfirm = new ResetPasswordDto()
            {
                Email = email,
                RedirectUrl = $"{BaseUrl}/Account/ConfirmEmail"
            };

            await _accountService.SendMailActivateAccountAsync(dtoConfirm);
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var response = await _accountService.ActivateAccountAssync(new ActivateAccountDto
            {
                Email = email,
                Token = token
            });

            if (response.StatusCode == HttpStatusCode.OK) return View(new ActivateAccountResultModel());

            return View(new ActivateAccountResultModel { ErrorMessage = response.GetErrors() });
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.RedirectUrl = $"{BaseUrl}/Account/ResetPassword";

                var model = VmToDto<ForgotPasswordViewModel, ResetPasswordDto>(vm);

                var response = await _accountService.SendMailForgotPasswordAsync(model);

                if (response.StatusCode == HttpStatusCode.OK) return RedirectToAction("ForgotPasswordConfirmation", "Account", vm);

                ModelState.AddModelError(string.Empty, response.GetErrors());
            }

            return View(vm);
        }

        public IActionResult ForgotPasswordConfirmation(ForgotPasswordViewModel vm)
        {
            return View();
        }

        [HttpGet]
        public async Task ResendEmailForgotPassword(string email)
        {
            var dtoConfirm = new ResetPasswordDto()
            {
                Email = email,
                RedirectUrl = $"{BaseUrl}/Account/ResetPassword"
            };

            await _accountService.SendMailForgotPasswordAsync(dtoConfirm);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = VmToDto<ResetPasswordViewModel, ForgotPasswordDto>(vm);

                var response = await _accountService.ForgotPasswordAsync(model);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("SignIn", "Account");
                }

                ModelState.AddModelError(string.Empty, response.GetErrors());
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Settings(string id)
        {
            var response = await _accountService.GetUserDetailByIdAsync(id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (!id.Equals(UserId))
                {
                    var children = await _accountService.GetChildrenAsync();

                    if (!children.Items.Any(x => x.Id.Equals(id)))
                    {
                        return Forbid();
                    }
                }

                var json = await response.Content.ReadAsStringAsync();

                var model = json.JsonToObj<UserDetailDto>();

                var vm = DtoToVm<UserDetailViewModel, UserDetailDto>(model);

                vm.SSN = string.Format("*** ** {0}", vm.SSN?.Substring(vm.SSN.Length - 5, 4) ?? "****");

                var states = await GetStates();

                vm.StateValue = vm.State;

                vm.State = states.FirstOrDefault(x => x.Code.Equals(vm.State))?.Id;

                ViewBag.States = new SelectList(states, "Id", "Name");

                if (vm.State != null)
                {
                    var cities = await GetCities(int.Parse(vm.State));

                    ViewBag.Cities = new SelectList(cities, "Id", "Name");
                }

                return View(vm);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(UserDetailViewModel viewModel)
        {
            var originState = viewModel.State;

            var states = await GetStates();

            ViewBag.States = new SelectList(states, "Id", "Name");

            if (viewModel.State != null)
            {
                var cities = await GetCities(int.Parse(viewModel.State));

                ViewBag.Cities = new SelectList(cities, "Id", "Name");
            }

            if (ModelState.IsValid)
            {
                viewModel.State = states.FirstOrDefault(x => x.Id.ToString().Equals(viewModel.State))?.Code;

                viewModel.StateValue = viewModel.State;

                var dto = VmToDto<UserDetailViewModel, EditProfileDto>(viewModel);

                var response = await _accountService.UpdateUserDetailByIdAsync(dto, viewModel.Id);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.JavaScriptFunction = string.Format("notificationManage.showSuccessMessage(\"{0}\");", "Saved!");
                }
                else
                {
                    ViewBag.JavaScriptFunction = string.Format("notificationManage.showErrorMessage(\"{0}\");", response.GetErrors());
                }
            }

            viewModel.State = originState;

            return View(viewModel);
        }

        public IActionResult ParentConfirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ParentConfirmation(ParentConfirmViewModel vm)
        {
            var response = await _accountService.ActivateAccountAssync(new ActivateAccountDto
            {
                Email = vm.Email,
                Token = vm.Token
            });

            if (response.StatusCode == HttpStatusCode.OK)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, response.GetErrors());

            return View();
        }

        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> AddEditChild(string id = null)
        {
            if (string.IsNullOrEmpty(id)) return View(new AddChildViewModel());

            var response = await _accountService.GetUserDetailByIdAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return View(json.JsonToObj<AddChildViewModel>());
            }

            if (response.StatusCode == HttpStatusCode.NotFound) return NotFound();

            return Redirect("/error/500");
        }

        [HttpPost]
        [Authorize(Roles = "Parent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditChild(AddChildViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(vm.Id))
                {
                    if (string.IsNullOrEmpty(vm.FirstName)) vm.FirstName = $"Baby";

                    if (string.IsNullOrEmpty(vm.LastName)) vm.LastName = $"{LastName}";

                    var createDto = VmToDto<AddChildViewModel, ChildInfoDto>(vm);

                    var createResponse = await _accountService.CreateChildAsync(createDto);

                    if (createResponse.StatusCode != HttpStatusCode.OK)
                    {
                        ModelState.AddModelError(string.Empty, createResponse.GetErrors());

                        return View(vm);
                    }

                    var createJson = await createResponse.Content.ReadAsStringAsync();

                    var createObj = createJson.JsonToObj<UserViewModel>();

                    vm.Id = createObj.Id;
                }

                if (vm.AvatarFile != null)
                {
                    var response = await _fileService.UploadFileAsync(vm.AvatarFile, vm.Id, UsingForType.Avatar);

                    var json = await response.Content.ReadAsStringAsync();

                    vm.Avatar = json;
                }

                if (vm.BannerFile != null)
                {
                    var response = await _fileService.UploadFileAsync(vm.BannerFile, vm.Id, UsingForType.Banner);

                    var json = await response.Content.ReadAsStringAsync();

                    vm.Banner = json;
                }

                var editDto = VmToDto<AddChildViewModel, EditProfileDto>(vm);

                var editResponse = await _accountService.UpdateUserDetailByIdAsync(editDto, vm.Id);

                if (editResponse.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Dashboard", "Account");

                ModelState.AddModelError(string.Empty, editResponse.GetErrors());
            }

            return View(vm);
        }

        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> Dashboard()
        {
            var children = await _accountService.GetChildrenAsync();

            return View(children.Items.Select(x => DtoToVm<UserDetailViewModel, UserDetailDto>(x)));
        }

        [HttpGet("{slug}/public")]
        public async Task<IActionResult> PublicPage(string slug)
        {
            var response = await _eventService.GetEventDetailByUserSlugAsync(slug);

            if (response.StatusCode == HttpStatusCode.NotFound) return NotFound();

            if (response.StatusCode == HttpStatusCode.Forbidden) return Forbid();

            var json = await response.Content.ReadAsStringAsync();

            var evt = json.JsonToObj<EventViewModel>();

            var donation = json.JsonToObj<DonationViewModel>();

            var vm = new PublicPageViewModel
            {
                Event = evt,
                Donation = donation
            };

            if (vm.Event.CreatedBy.Equals(UserId)) vm.IsEdit = true;

            return View(vm);
        }

        [Authorize(Roles = "Parent")]
        [HttpGet("{slug}/public/preview")]
        public async Task<IActionResult> PreviewPublicPage(string slug)
        {
            var response = await _eventService.GetEventDetailByUserSlugAsync(slug);

            if (response.StatusCode == HttpStatusCode.NotFound) return NotFound();

            if (response.StatusCode == HttpStatusCode.Forbidden) return Forbid();

            var json = await response.Content.ReadAsStringAsync();

            var evt = json.JsonToObj<DonationViewModel>();

            var vm = new PublicPageViewModel
            {
                Donation = evt
            };

            return View("~/Views/Account/PublicPage.cshtml", vm);
        }

        [HttpPost]
        [ActionName("give")]
        public async Task<JsonResult> Give(DonationViewModel vm)
        {
            var dto = VmToDto<DonationViewModel, DonationDto>(vm);

            var response = await _accountService.DonateAsync(dto.UserId, dto);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();

                var model = json.JsonToObj<DonationResultDto>();

                return Json(new { success = model.IsSuccess });
            }

            return Json(new { success = false });
        }

        public async Task<JsonResult> GetCitiesByState(int stateId)
        {
            return Json(await GetCities(stateId));
        }

        [ResponseCache]
        private async Task<IEnumerable<State>> GetStates()
        {
            const int usId = 231;

            var responseStates = await _infrastructorService.GetStatesByCountryId(usId);

            if (responseStates.StatusCode == HttpStatusCode.OK)
            {
                var jsonStates = await responseStates.Content.ReadAsStringAsync();

                var states = jsonStates.JsonToObj<IEnumerable<State>>();

                return states;
            }

            return null;
        }

        private async Task<IEnumerable<City>> GetCities(int stateId)
        {
            var responsecities = await _infrastructorService.GetCitiesByStateId(stateId);

            if (responsecities.StatusCode == HttpStatusCode.OK)
            {
                var json = await responsecities.Content.ReadAsStringAsync();

                var cities = json.JsonToObj<IEnumerable<City>>().Select(x => new City { Id = x.Name, Name = x.Name });

                return cities;

            }
            return null;
        }
    }
}
