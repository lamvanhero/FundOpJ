using FluentValidation;

namespace OppJar.Dto
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();

            RuleFor(x => x.RedirectUrl).NotNull().NotEmpty();
        }
    }
}
