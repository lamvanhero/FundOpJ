using System.Threading.Tasks;
using OppJar.Dto;

namespace OppJar.Core.Services
{
    public interface IEventService
    {
        Task<EventDto> EditEventAsync(string id, AddEditEventDto dto);

        Task<EventDto> GetEventByUserSlugAsync(string slug);
    }
}
