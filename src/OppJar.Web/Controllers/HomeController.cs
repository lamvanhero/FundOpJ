using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OppJar.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMapper mapper, IConfiguration configuration) : base(mapper, configuration)
        {
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("SuperAdministrator") || User.IsInRole("Administrator")) return RedirectToAction("Index", "Home", new { area = "Admins" });

                if (User.IsInRole("Parent")) return RedirectToAction("Dashboard", "Account");

                return RedirectToAction("Index", "Give");
            }

            return RedirectToAction("SignIn", "Account");
        }
    }
}
