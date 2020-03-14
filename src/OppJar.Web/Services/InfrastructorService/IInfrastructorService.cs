using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public interface IInfrastructorService
    {
        Task<HttpResponseMessage> GetStatesByCountryId(int countryId);

        Task<HttpResponseMessage> GetCitiesByStateId(int stateId);
    }
}
