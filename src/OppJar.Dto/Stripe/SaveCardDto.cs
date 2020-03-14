using FluentValidation;

namespace OppJar.Dto
{
    public class SaveCardDto
    {
        public string CustomerId { get; set; }
        public string PaymentMethodIds { get; set; }
    }

    public class SaveCardDtoValidator : AbstractValidator<SaveCardDto>
    {
        public SaveCardDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.PaymentMethodIds).NotNull().NotEmpty();
        }
    }
}
