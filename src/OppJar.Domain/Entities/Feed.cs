using OppJar.Common.Enum;

namespace OppJar.Domain
{
    public class Feed : BaseEntity, IBaseEntity, IAudit
    {
        public string Content { get; set; }
        public string UserSlug { get; set; }
        public string FileUrl { get; set; }
        public int Like { get; set; }
        public string Preview { get; set; }
        public FeedType FeedType { get; set; } = FeedType.Text;
        public Privacy Privacy { get; set; } = Privacy.Private;
    }
}
