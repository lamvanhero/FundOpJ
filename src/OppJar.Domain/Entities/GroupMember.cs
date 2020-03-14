namespace OppJar.Domain
{
    public class GroupMember : IBaseEntity
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public bool IsOwner { get; set; }
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
