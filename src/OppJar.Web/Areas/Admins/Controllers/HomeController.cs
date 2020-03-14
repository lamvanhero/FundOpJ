using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Helpers;
using OppJar.Common.Enum;
using OppJar.Dto;
using OppJar.Web.Controllers;
using OppJar.Web.Enums;
using OppJar.Web.Helpers;
using OppJar.Web.Models;
using OppJar.Web.Services;

namespace OppJar.Web.Areas.Admin.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = "SuperAdministrator, Administrator")]
    public class HomeController : BaseController
    {
        private readonly IAccountService _accountService;

        public HomeController(IMapper mapper, IAccountService accountService, IConfiguration configuration) : base(mapper, configuration)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("admins/users/search")]
        public async Task<JsonResult> SearchAccount([DataTablesRequest] DataTablesRequest dataRequest)
        {
            var dto = new AccountQuerySearch()
            {
                Page = (dataRequest.Start / dataRequest.Length) + 1,
                Size = dataRequest.Length,
                UserType = UserType.Administrator
            };

            if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            {
                dto.SearchKey = dataRequest.Search?.Value;
            }

            var response = await _accountService.SearchAsync(dto);

            return Json(response.Items.ToDataTablesResponse(dataRequest, response.TotalRecord, response.TotalRecord));
        }

        [HttpPost("admins/users/{id}/{status}")]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<JsonResult> UpdateUserStatus(string id, UserStatus status)
        {
            var response = await _accountService.ChangeStatusAsync(status, id);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { error = response.GetErrors() });
            }

            return Json(new { success = true });
        }

        [HttpGet("admins/users/{id?}")]
        public async Task<ActionResult> EditApplicationUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return PartialView("~/Areas/Admins/Views/Home/_AddEditAdmin.cshtml", new AddEditAdminViewModel());

            var response = await _accountService.GetUserDetailByIdAsync(id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();

                var vm = json.JsonToObj<AddEditAdminViewModel>();

                vm.ActionMode = ActionMode.Edit;

                return PartialView("~/Areas/Admins/Views/Home/_AddEditAdmin.cshtml", vm);
            }

            return NotFound();
        }

        [HttpPost("admins/users/{id?}")]
        public async Task<ActionResult> EditApplicationUser(string id, AddEditAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(id))
                {
                    var dto = VmToDto<AddEditAdminViewModel, BaseRegisterDto>(vm);

                    var response = await _accountService.CreateAdminAsync(dto);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var dtoConfirm = new ResetPasswordDto()
                        {
                            Email = json.JsonToObj<UserDto>().Email,
                            RedirectUrl = $"{BaseUrl}/Account/ConfirmEmail"
                        };

                        await _accountService.SendMailActivateAccountAsync(dtoConfirm);

                        return Json(new { success = true });
                    }

                    ModelState.AddModelError(string.Empty, response.GetErrors());
                }
                else
                {
                    var model = VmToDto<AddEditAdminViewModel, EditProfileDto>(vm);

                    var response = await _accountService.UpdateUserDetailByIdAsync(model, id);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Json(new { success = true });
                    }

                    ModelState.AddModelError(string.Empty, response.GetErrors());
                }
            }

            return PartialView("~/Areas/Admins/Views/Home/_AddEditAdmin.cshtml", vm);
        }
    }
}