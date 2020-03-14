using OppJar.Common.Enum;

namespace OppJar.Domain
{
    public class Group : BaseEntity, IBaseEntity, IAudit
    {
        public GroupType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Banner { get; set; }
    }
}
