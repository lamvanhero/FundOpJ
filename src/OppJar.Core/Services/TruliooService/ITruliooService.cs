using Newtonsoft.Json.Linq;
using OppJar.Core.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OppJar.Core.Services
{
    public interface ITruliooService
    {
        Task<IEnumerable<string>> GetCountryCodesAsync();
        Task<JObject> GetFieldsAsync(string countryCode);
        Task<IEnumerable<string>> GetConsentsAsync(string countryCode);
        Task<VerifyResult> VerifyAsync(VerifyRequest request);
        Task<string> TestConnectionAsync();
    }
}
