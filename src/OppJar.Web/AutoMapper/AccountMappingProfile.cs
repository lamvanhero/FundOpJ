using AutoMapper;
using OppJar.Dto;
using OppJar.Web.Models;

namespace OppJar.Web.AutoMapper
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            #region ViewModel to Dto

            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<ForgotPasswordViewModel, ResetPasswordDto>();
            CreateMap<ResetPasswordViewModel, ForgotPasswordDto>();
            CreateMap<UserDetailViewModel, EditProfileDto>();
            CreateMap<AddEditAdminViewModel, BaseRegisterDto>();
            CreateMap<AddEditAdminViewModel, EditProfileDto>();
            CreateMap<RegisterViewModel, BaseRegisterDto>();
            CreateMap<EditProfileViewModel, EditProfileDto>();
            CreateMap<ForgotPasswordViewModel, ResetPasswordDto>();
            CreateMap<ResetPasswordViewModel, ForgotPasswordDto>();
            CreateMap<UserViewModel, UserDetailDto>();
            CreateMap<UserViewModel, EditProfileDto>();
            CreateMap<DonationViewModel, DonationDto>();
            CreateMap<AddChildViewModel, ChildInfoDto>();
            CreateMap<AddChildViewModel, EditProfileDto>();
            #endregion

            #region Dto to ViewModel
            CreateMap<UserDetailDto, UserDetailViewModel>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Status = src.Status.Equals("Activate") ? true : false;
                });
            CreateMap<EditProfileDto, EditProfileViewModel>();
            CreateMap<UserDetailDto, UserViewModel>();
            CreateMap<UserDetailDto, AddEditAdminViewModel>();
            CreateMap<EditProfileDto, UserViewModel>();
            CreateMap<UserDto, RegisterViewModel>();
            #endregion
        }
    }
}
