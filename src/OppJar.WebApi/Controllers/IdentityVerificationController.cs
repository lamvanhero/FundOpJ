using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OppJar.Core.Services;
using OppJar.Core.Services.Model;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    [Route("api/verification")]
    [AllowAnonymous]
    public class IdentityVerificationController : BaseApi
    {
        private readonly ITruliooService _truliooService;

        public IdentityVerificationController(ITruliooService truliooService)
        {
            _truliooService = truliooService;
        }

        [HttpGet]
        public async Task<IActionResult> CheckConnection()
        {
            return Ok(await _truliooService.TestConnectionAsync());
        }

        [HttpGet("country-codes")]
        public async Task<IActionResult> GetCountryCodes()
        {
            return Ok(await _truliooService.GetCountryCodesAsync());
        }

        [HttpGet("{countryCode}/fields")]
        public async Task<IActionResult> GetFields(string countryCode)
        {
            return Ok(await _truliooService.GetFieldsAsync(countryCode));
        }

        [HttpGet("{countryCode}/consents")]
        public async Task<IActionResult> GetConsents(string countryCode)
        {
            return Ok(await _truliooService.GetConsentsAsync(countryCode));
        }

        [HttpPost]
        public async Task<IActionResult> Verify([FromBody] VerifyRequest verifyRequest)
        {
            return Ok(await _truliooService.VerifyAsync(verifyRequest));
        }
    }
}
