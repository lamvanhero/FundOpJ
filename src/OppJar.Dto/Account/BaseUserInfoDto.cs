using FluentValidation;
using System;

namespace OppJar.Dto
{
    public class BaseUserInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; } = DateTime.UtcNow;
    }

    public class BaseUserInfoDtoValidator<T> : AbstractValidator<T> where T : BaseUserInfoDto
    {
        public BaseUserInfoDtoValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();

            RuleFor(x => x.LastName).NotNull().NotEmpty();

            RuleFor(x => x.DOB).NotNull().NotEmpty();
        }
    }
}
