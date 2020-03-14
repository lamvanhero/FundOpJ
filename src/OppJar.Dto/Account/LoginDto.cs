using FluentValidation;

namespace OppJar.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();

            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}