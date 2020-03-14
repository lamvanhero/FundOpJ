using Microsoft.AspNetCore.Mvc;

namespace OppJar.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/403")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [Route("error/404")]
        public IActionResult NotFoundObject()
        {
            return View();
        }

        [Route("error/500")]
        public IActionResult InternalServer()
        {
            return View();
        }

        [Route("error/401")]
        public IActionResult UnAuthorized()
        {
            return RedirectToAction("SignIn", "Account");
        }
    }
}
