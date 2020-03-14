using AutoMapper;
using OppJar.Domain;
using OppJar.Dto;

namespace OppJar.AutoMapper
{
    public class FeedMapperProfile : Profile
    {
        public FeedMapperProfile()
        {
            CreateMap<CreateFeedDto, Feed>();

            CreateMap<Feed, FeedDto>();
        }
    }

    public static class FeedMapper
    {
        static FeedMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new FeedMapperProfile()))
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Feed ToEntity(this CreateFeedDto dto)
        {
            return Mapper.Map<Feed>(dto);
        }

        public static FeedDto ToDto(this Feed entity)
        {
            return Mapper.Map<FeedDto>(entity);
        }
    }
}
