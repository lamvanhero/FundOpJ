using AutoMapper;
using OppJar.Dto;
using OppJar.Web.Models;

namespace OppJar.Web.AutoMapper
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            #region ViewModel to Dto
            CreateMap<EventViewModel, AddEditEventDto>();

            #endregion
            CreateMap<AddEditEventDto, EventViewModel>();

        }
    }
}
