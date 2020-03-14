using FluentValidation;
using OppJar.Common.Enum;

namespace OppJar.Dto
{
    public class CreateFeedDto
    {
        public string UserSlug { get; set; }
        public string Content { get; set; }
        public FeedType FeedType { get; set; } = FeedType.Text;
        public string FileUrl { get; set; }
        public string Preview { get; set; }
    }

    public class CreatePostDtoValidator : AbstractValidator<CreateFeedDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Content).NotNull().NotEmpty();
            RuleFor(x => x.UserSlug).NotNull().NotEmpty();
        }
    }
}
