using System.Threading.Tasks;
using OppJar.Domain.Enum;
using OppJar.Dto;
using System.Net.Http;

namespace OppJar.Web.Services
{
    public interface IEventService
    {
        Task<HttpResponseMessage> GetEventDetailByUserSlugAsync(string slug);

        Task<HttpResponseMessage> UpdateEventAsync(AddEditEventDto dto, string id);

        Task<HttpResponseMessage> ChangeEventStatusAsync(EventStatus status, string id);
    }
}
