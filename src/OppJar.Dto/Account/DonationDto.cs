
using FluentValidation;

namespace OppJar.Dto
{
    public class DonationDto
    {
        public string SenderName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public decimal Amount { get; set; }
    }

    public class DonationDtoValidator : AbstractValidator<DonationDto>
    {
        public DonationDtoValidator()
        {
            RuleFor(x => x.SenderName).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.")
                .LessThan(500).WithMessage("Amount must be less than 500$.");
        }
    }

}