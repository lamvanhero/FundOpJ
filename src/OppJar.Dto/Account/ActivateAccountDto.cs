using FluentValidation;

namespace OppJar.Dto
{
    public class ActivateAccountDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ActivateAccountDtoValidator : AbstractValidator<ActivateAccountDto>
    {
        public ActivateAccountDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();

            RuleFor(x => x.Token).NotNull().NotEmpty();
        }
    }
}
