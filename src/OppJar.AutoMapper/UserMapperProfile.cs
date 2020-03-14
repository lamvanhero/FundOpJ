using AutoMapper;
using OppJar.Common.Helpers;
using OppJar.Domain;
using OppJar.Common.Enum;
using OppJar.Dto;
using System;

namespace OppJar.AutoMapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Slug = SlugHelper.GenerateSlug($"{src.FirstName} {src.LastName}");
                    dest.UserName = src.Email;
                    dest.NormalizedUserName = src.Email.ToUpper();
                });

            CreateMap<ChildInfoDto, User>()
                .AfterMap((src, dest) =>
                {
                    var tempEmail = $"{SlugHelper.GenerateSlug($"{src.FirstName} {src.LastName}")}@oppjar.com";

                    dest.UserName = tempEmail;
                    dest.Email = tempEmail;
                    dest.NormalizedUserName = tempEmail.ToUpper();
                    dest.Slug = SlugHelper.GenerateSlug($"{src.FirstName} {src.LastName}");
                });

            CreateMap<EditProfileDto, UserDetail>()
                .AfterMap((src, dest) =>
                {
                    Enum.TryParse(src.Privacy, out Privacy privacy);
                    dest.Privacy = privacy;
                })
                .ForAllMembers(opt => opt.IgnoreSourceWhenDefault());

            CreateMap<EditProfileDto, Address>()
                .ForAllMembers(opt => opt.IgnoreSourceWhenDefault());

            CreateMap<User, UserDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.UserDetail.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.UserDetail.LastName))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(src => src.UserDetail.MiddleName));

            CreateMap<User, UserDetailDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.UserDetail.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.UserDetail.LastName))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(src => src.UserDetail.MiddleName))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(src => src.UserDetail.Avatar))
                .ForMember(x => x.Banner, opt => opt.MapFrom(src => src.UserDetail.Banner))
                .ForMember(x => x.About, opt => opt.MapFrom(src => src.UserDetail.About))
                .ForMember(x => x.DOB, opt => opt.MapFrom(src => src.UserDetail.DOB))
                .ForMember(x => x.SSN, opt => opt.MapFrom(src => src.UserDetail.SSN))
                .ForMember(x => x.Slug, opt => opt.MapFrom(src => src.Slug))
                .ForMember(x => x.TotalBalance, opt => opt.MapFrom(src => src.TotalBalance))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(UserStatus), src.Status)))
                .ForMember(x => x.Privacy, opt => opt.MapFrom(src => Enum.GetName(typeof(Privacy), src.UserDetail.Privacy)))
                .ForMember(x => x.PrimaryAddress, opt => opt.MapFrom(src => src.Address.PrimaryAddress))
                .ForMember(x => x.SecondAddress, opt => opt.MapFrom(src => src.Address.SecondAddress))
                .ForMember(x => x.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(x => x.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(x => x.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(x => x.UserType, opt => opt.MapFrom(src => src.UserRole.Role.Name))
                .ForMember(x => x.School, opt => opt.MapFrom(src => src.UserDetail.School));

            CreateMap<UserDto, User>();
        }
    }

    public static class UserMapper
    {
        static UserMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile(new UserMapperProfile()))
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static User ToEntity(this RegisterDto dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static User ToEntity(this ChildInfoDto dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static UserDetail ToDetailEntity(this EditProfileDto dto, UserDetail entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static Address ToAddressEntity(this EditProfileDto dto, Address entity)
        {
            return Mapper.Map(dto, entity);
        }

        public static UserDto ToDto(this User entity)
        {
            return Mapper.Map<UserDto>(entity);
        }

        public static UserDetailDto ToDetailDto(this User entity)
        {
            return Mapper.Map<UserDetailDto>(entity);
        }

        public static User ToEntity(this UserDto dto)
        {
            return Mapper.Map<User>(dto);
        }
    }
}
