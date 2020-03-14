using System;
using AutoMapper;
using OppJar.Common.Helpers;
using OppJar.Domain;
using OppJar.Domain.Enum;
using OppJar.Dto;

namespace OppJar.AutoMapper
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {

            CreateMap<Event, EventDto>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(EventStatus), src.Status)));

            CreateMap<EventDto, Event>();

            CreateMap<AddEditEventDto, Event>()
                .ForAllMembers(opt => opt.IgnoreSourceWhenDefault());
        }
    }

    public static class EventMapper
    {
        static EventMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new EventMapperProfile()))
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static EventDto ToEventDto(this Event entity)
        {
            return Mapper.Map<EventDto>(entity);
        }

        public static Event ToEntity(this EventDto dto)
        {
            return Mapper.Map<Event>(dto);
        }
        public static Event DtoToEntity(this AddEditEventDto dto)
        {
            return Mapper.Map<Event>(dto);
        }

        public static Event ToEventEntity(this AddEditEventDto dto, Event entity)
        {
            return Mapper.Map(dto, entity);
        }


    }
}
