using System;

namespace OppJar.Web.Models
{
    public class UserDetailViewModel : ParentInfoViewModel
    {
        public string Avatar { get; set; }
        public string Banner { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public string Privacy { get; set; }
        public decimal TotalBalance { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Slug { get; set; }
        public DateTime DOB { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public bool Status { get; set; }
    }
}
