using AutoMapper;
using OppJar.Domain;
using OppJar.Dto;

namespace OppJar.AutoMapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<CreateEditCommentDto, Comment>()
                .ForAllMembers(opt => opt.IgnoreSourceWhenDefault());

            CreateMap<Comment, CommentDto>();
        }
    }

    public static class CommentMapper
    {
        static CommentMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new CommentMapperProfile()))
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static Comment ToEntity(this CreateEditCommentDto dto)
        {
            return Mapper.Map<Comment>(dto);
        }

        public static Comment ToEntity(this CreateEditCommentDto dto, Comment entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static CommentDto ToDto(this Comment entity)
        {
            return Mapper.Map<CommentDto>(entity);
        }
    }
}
