namespace OppJar.Domain
{
    public class WalletDetail : IBaseEntity
    {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
