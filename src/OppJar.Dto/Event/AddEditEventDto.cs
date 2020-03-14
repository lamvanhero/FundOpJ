using FluentValidation;

namespace OppJar.Dto
{
    public class AddEditEventDto
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlPhotos { get; set; }
        public string NiceUrl { get; set; }
        public string Status { get; set; }
    }

    public class AddEditEventDtoValidator : AbstractValidator<AddEditEventDto>
    {
        public AddEditEventDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.UrlPhotos).NotNull().NotEmpty();
        }
    }
}
