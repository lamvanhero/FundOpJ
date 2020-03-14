using FluentValidation;

namespace OppJar.Dto
{
    public class CreateEditCommentDto
    {
        public string Content { get; set; }
        public string FileUrl { get; set; }
        public string ParentId { get; set; }
        public string ReferId { get; set; }
    }

    public class CreateEditCommentDtoValidator : AbstractValidator<CreateEditCommentDto>
    {
        public CreateEditCommentDtoValidator()
        {
            RuleFor(x => x.Content).NotNull().NotEmpty();
            RuleFor(x => x.ReferId).NotNull().NotEmpty();
        }
    }
}
