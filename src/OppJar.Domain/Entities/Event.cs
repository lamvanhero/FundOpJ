using OppJar.Domain.Enum;

namespace OppJar.Domain
{
    public class Event : BaseEntity, IBaseEntity, IAudit
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlPhotos { get; set; }
        public string NiceUrl { get; set; }
        public EventStatus Status { get; set; } = EventStatus.Activate;
        public virtual User User { get; set; }

        public Event() { }

        public Event(string userId)
        {
            UserId = userId;
        }
    }
}
