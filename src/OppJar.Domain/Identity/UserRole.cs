namespace OppJar.Domain
{
    public class UserRole : IBaseEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
