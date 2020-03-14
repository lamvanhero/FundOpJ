using OppJar.Common.TokenSerializer;
using OppJar.Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace OppJar.Domain
{
    public class User : BaseEntity, IBaseEntity, IAudit, ITokenSecurity
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Deactivate;
        public string Slug { get; set; }
        public string SecurityStamp { get; set; }
        public decimal TotalBalance { get; set; }
        public string EventId { get; set; }
        [NotMapped]
        public string Key => Id;
        public virtual UserDetail UserDetail { get; set; }
        public virtual Address Address { get; set; }
        public virtual UserRole UserRole { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
