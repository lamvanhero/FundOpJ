using Microsoft.AspNetCore.Mvc;
using OppJar.Common.Helpers;
using OppJar.Core.Settings;
using OppJar.Domain;
using OppJar.WebApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfrastructorController : ControllerBase
    {
        private readonly DbSeed _dbSeed;

        public InfrastructorController(DbSeed dbSeed)
        {
            _dbSeed = dbSeed;
        }

        [HttpGet("seed/data")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SeedData()
        {
            await _dbSeed.SeedRole(UserRoles.FindAll());

            await _dbSeed.SeedAdmin();

            return Ok("Done");
        }

        [HttpGet("configurations/countries")]
        public IActionResult GetCountries()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "countries.json");

            using StreamReader reader = System.IO.File.OpenText(path);

            var content = reader.ReadToEnd();

            return Ok(content.JsonToObj<IEnumerable<Country>>());
        }

        [HttpGet("configurations/states/{countryId}")]
        public IActionResult GetStates(int countryId = 231)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "states.json");

            using StreamReader reader = System.IO.File.OpenText(path);

            var content = reader.ReadToEnd();

            var states = content.JsonToObj<IEnumerable<State>>();

            return Ok(states.Where(x => x.CountryId.Equals(countryId.ToString())));
        }

        [HttpGet("configurations/cities/{stateId}")]
        public IActionResult GetCities(int stateId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cities.json");

            using StreamReader reader = System.IO.File.OpenText(path);

            var content = reader.ReadToEnd();

            var cities = content.JsonToObj<IEnumerable<City>>();

            return Ok(cities.Where(x => x.StateId.Equals(stateId.ToString())));
        }
    }
}
