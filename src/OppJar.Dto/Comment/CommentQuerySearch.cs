using FluentValidation;

namespace OppJar.Dto
{
    public class CommentQuerySearch : QuerySearchDefault
    {
        public string ReferId { get; set; }
    }

    public class CommentQuerySearchValidator : AbstractValidator<CommentQuerySearch>
    {
        public CommentQuerySearchValidator()
        {
            RuleFor(x => x.ReferId).NotNull().NotEmpty();
        }
    }
}
