using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OppJar.Web.Models;
using OppJar.Web.Proxies;

namespace OppJar.Web.Services
{
    public class InfrastructorService : ServiceBase, IInfrastructorService
    {
        private const string GET_STATES_BY_COUNTRY_ID = "/api/infrastructor/configurations/states/{0}";
        private const string GET_CITY_BY_STATE_ID = "/api/infrastructor/configurations/cities/{0}";

        public InfrastructorService(IHttpContextAccessor httpContextAccessor, OppJarProxy oppJarProxy) : base(httpContextAccessor, oppJarProxy)
        {
        }

        public async Task<HttpResponseMessage> GetStatesByCountryId(int countryId)
        {
            return await _oppJarProxy.GetAsync(string.Format(GET_STATES_BY_COUNTRY_ID,countryId));
        }

        public async Task<HttpResponseMessage> GetCitiesByStateId(int stateId)
        {
            return await _oppJarProxy.GetAsync(string.Format(GET_CITY_BY_STATE_ID, stateId));
        }
    }
}
