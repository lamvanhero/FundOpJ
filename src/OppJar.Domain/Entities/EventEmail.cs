namespace OppJar.Domain
{
    public class EventEmail : IBaseEntity
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string UserEmail { get; set; }
        public virtual Event Event { get; set; }
    }
}
