using FluentValidation;
using OppJar.Common.Enum;
using System;

namespace OppJar.Dto
{
    public class EditBaseProfileDto
    {
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Avatar { get; set; }
        public string Banner { get; set; }
    }

    public class EditBaseProfileDtoValidtor<T> : AbstractValidator<T> where T : EditBaseProfileDto
    {
        public EditBaseProfileDtoValidtor()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().When(x => x.UserType != UserType.Parent && x.UserType != UserType.Giver);
            RuleFor(x => x.LastName).NotNull().NotEmpty().When(x => x.UserType != UserType.Parent && x.UserType != UserType.Giver);
            RuleFor(x => x.DOB).NotNull().NotEmpty().When(x => x.UserType == UserType.Child);
        }
    }
}
