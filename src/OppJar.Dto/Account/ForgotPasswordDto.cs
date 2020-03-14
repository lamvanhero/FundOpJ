using FluentValidation;

namespace OppJar.Dto
{
    public class ForgotPasswordDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordDtoValidtor : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidtor()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();

            RuleFor(x => x.Token).NotNull().NotEmpty();

            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
