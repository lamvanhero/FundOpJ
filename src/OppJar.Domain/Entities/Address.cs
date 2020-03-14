namespace OppJar.Domain
{
    public class Address : BaseEntity, IBaseEntity, IAudit
    {
        public string UserId { get; set; }
        public string PrimaryAddress { get; set; }
        public string SecondAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public virtual User User { get; set; }
    }
}
