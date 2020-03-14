using OppJar.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace OppJar.Web.Services
{
    public interface IMemoryService
    {
        Task<HttpResponseMessage> SearchAsync(FeedQuerySearch querySearch);
        Task<HttpResponseMessage> GetByIdAsync(string id, string slug);
    }
}
