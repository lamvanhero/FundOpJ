using OppJar.Common.Enum;
using System;

namespace OppJar.Domain
{
    public class UserDetail : BaseEntity, IBaseEntity, IAudit
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string About { get; set; }
        public DateTime DOB { get; set; }
        public Sex Sex { get; set; }
        public string SSN { get; set; }
        public string TaxId { get; set; }
        public string CustomerId { get; set; }
        public string PortfolioId { get; set; }
        public string BankName { get; set; }
        public string BankAccNumber { get; set; }
        public string BankRoutingNumber { get; set; }
        public string School { get; set; }
        public string GPA { get; set; }
        public string Avatar { get; set; } = "/assets/no_image.png";
        public string Banner { get; set; }
        public Privacy Privacy { get; set; }
        public virtual User User { get; set; }
    }
}
