using FluentValidation;

namespace OppJar.Dto
{
    public class BaseRegisterDto : BaseUserInfoDto
    {
        public string Email { get; set; }
        public string Password { get; set; } = "Oppj@r2020";
        public string SSN { get; set; }
        public string PrimaryAddress { get; set; }
        public string SecondAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class BaseRegisterDtoValidator<T> : BaseUserInfoDtoValidator<T> where T: BaseRegisterDto
    {
        public BaseRegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
    }
}
