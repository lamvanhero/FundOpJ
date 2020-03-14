using FluentValidation;

namespace OppJar.Dto
{
    public class CreateCustomerDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
