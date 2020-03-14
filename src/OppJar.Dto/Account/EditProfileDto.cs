using FluentValidation;
using OppJar.Common.Enum;

namespace OppJar.Dto
{
    public class EditProfileDto : EditBaseProfileDto
    {
        public string PrimaryAddress { get; set; }
        public string SecondAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string About { get; set; }
        public string Privacy { get; set; }
        public string School { get; set; }
    }

    public class EditProfileDtoValidator : EditBaseProfileDtoValidtor<EditProfileDto>
    {
        public EditProfileDtoValidator()
        {
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
        }
    }
}
