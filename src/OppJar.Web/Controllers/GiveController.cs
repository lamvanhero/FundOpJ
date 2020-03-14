using AutoMapper;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OppJar.Common.Enum;
using OppJar.Dto;
using OppJar.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OppJar.Web.Controllers
{
    public class GiveController : BaseController
    {
        private readonly IAccountService _accountService;

        public GiveController(IMapper mapper, IConfiguration configuration, IAccountService accountService) : base(mapper, configuration)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("children/search")]
        public async Task<JsonResult> SearchAccount([DataTablesRequest] DataTablesRequest dataRequest)
        {
            if (!string.IsNullOrEmpty(dataRequest?.Search?.Value))
            {
                var dto = new AccountQuerySearch()
                {
                    Page = (dataRequest.Start / dataRequest.Length) + 1,
                    Size = dataRequest.Length,
                    UserType = UserType.Child,
                    SearchKey = dataRequest.Search.Value
                };

                var response = await _accountService.SearchAsync(dto);

                return Json(response.Items.ToDataTablesResponse(dataRequest, response.TotalRecord, response.TotalRecord));
            }

            return Json(new List<UserDetailDto>().ToDataTablesResponse(dataRequest, 0, 0));
        }
    }
}
