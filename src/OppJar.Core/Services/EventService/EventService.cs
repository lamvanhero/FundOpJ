using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OppJar.AutoMapper;
using OppJar.Core.Exceptions;
using OppJar.Dto;
using Microsoft.Extensions.Configuration;

namespace OppJar.Core.Services
{
    public class EventService : ServiceBase, IEventService
    {
        private readonly IAccountService _accountService;

        public EventService(IUnitOfWork unitOfWork, IAccountService accountService, IConfiguration configuration) : base(unitOfWork, configuration)
        {
            _accountService = accountService;
        }

        public async Task<EventDto> EditEventAsync(string id, AddEditEventDto dto)
        {
            var evt = await _unitOfWork.EventRepository.FindAll(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (evt == null) throw new NotFoundException("Not found event.");

            evt.Title = dto.Title;
            evt.Description = dto.Description;
            evt.UrlPhotos = dto.UrlPhotos;

            _unitOfWork.EventRepository.Update(evt);

            await _unitOfWork.CommitAsync();

            return evt.ToEventDto();
        }

        public async Task<EventDto> GetEventByUserSlugAsync(string slug)
        {
            var user = await _accountService.GetUserDetailBySlugAsync(slug);

            var evt = await _unitOfWork.EventRepository.FindAll(x => x.UserId.Equals(user.Id)).SingleOrDefaultAsync();

            if (evt == null) throw new NotFoundException("Not found event.");

            return evt.ToEventDto();
        }
    }
}
