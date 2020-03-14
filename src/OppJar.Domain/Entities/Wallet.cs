namespace OppJar.Domain
{
    public class Wallet : BaseEntity, IBaseEntity, IAudit
    {
        public string Description { get; set; }
        public decimal Balance { get; set; }
    }
}
