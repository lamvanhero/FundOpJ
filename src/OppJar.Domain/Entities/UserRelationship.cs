using OppJar.Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace OppJar.Domain
{
    public class UserRelationship : IBaseEntity
    {
        public string Id { get; set; }
        public string UserId1 { get; set; }
        public RelationshipLevel User1Level { get; set; }
        public string UserId2 { get; set; }
        public RelationshipLevel User2Level { get; set; }

        [ForeignKey("UserId1")]
        public virtual User User1 { get; set; }
        [ForeignKey("UserId2")]
        public virtual User User2 { get; set; }
    }
}
