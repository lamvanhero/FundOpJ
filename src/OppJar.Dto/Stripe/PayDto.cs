using FluentValidation;

namespace OppJar.Dto
{
    public class PayDto
    {
        public double Amount { get; set; }
        public ConfirmPaymentRequestDto ConfirmPaymentRequest { get; set; }
    }

    public class PayDtoValidator : AbstractValidator<PayDto>
    {
        public PayDtoValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).LessThanOrEqualTo(500);
        }
    }
}
