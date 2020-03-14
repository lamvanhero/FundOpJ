using FluentValidation;

namespace OppJar.Dto
{
    public class EventQuerySearch : QuerySearchDefault
    {
        public string UserId { get; set; }
    }

    public class EventQuerySearchValidator : AbstractValidator<EventQuerySearch>
    {
        public EventQuerySearchValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}
