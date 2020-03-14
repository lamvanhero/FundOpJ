using FluentValidation;
using OppJar.Common.Enum;
using System;

namespace OppJar.Dto
{
    public class RegisterDto : BaseRegisterDto
    {
        public UserType UserType { get; set; } = UserType.Parent;
    }

    public class RegisterDtoAbstract : BaseRegisterDtoValidator<RegisterDto>
    {
        public RegisterDtoAbstract()
        {
            RuleFor(x => x.SSN)
                .MinimumLength(9)
                .MaximumLength(9)
                .NotNull()
                .NotEmpty()
                .When(x => x.UserType == UserType.Parent);

            RuleFor(x => x.PrimaryAddress)
                .NotNull()
                .NotEmpty()
                .When(x => x.UserType == UserType.Parent);

            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
                .When(x => x.UserType == UserType.Parent);

            RuleFor(x => x.State)
                .NotNull()
                .NotEmpty()
                .When(x => x.UserType == UserType.Parent);

            RuleFor(x => x.ZipCode)
                .NotNull()
                .NotEmpty()
                .When(x => x.UserType == UserType.Parent);

            RuleFor(x => x.DOB)
                .NotNull()
                .NotEmpty()
                .LessThan(DateTime.Now);
        }
    }
}
