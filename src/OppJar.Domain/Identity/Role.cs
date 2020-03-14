namespace OppJar.Domain
{
    public class Role : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
    }
}
