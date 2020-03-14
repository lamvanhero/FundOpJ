using OppJar.Common.Enum;
using System;

namespace OppJar.Domain
{
    public class Transaction : BaseEntity, IBaseEntity, IAudit
    {
        public string TransactionId { get; set; }
        public TransactionType Type { get; set; }
        public string UserId { get; set; }
        public TransactionSource Source { get; set; }
        public string ControlNumber { get; set; }
        public string GroupId { get; set; }
        public string AcctNumber { get; set; }
        public DateTime PostingDate { get; set; }
        public JournalType DbCrIndicator { get; set; }
        public string Amout { get; set; }
        public string Description { get; set; }
        public string Memo { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
