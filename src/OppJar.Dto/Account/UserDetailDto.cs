using System;

namespace OppJar.Dto
{
    public class UserDetailDto : UserDto
    {
        public string Avatar { get; set; }
        public string Banner { get; set; }
        public string PrimaryAddress { get; set; }
        public string SecondAddress { get; set; }
        public string SSN { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string About { get; set; }
        public string Privacy { get; set; }
        public decimal TotalBalance { get; set; }
        public DateTime DOB { get; set; }
        public string School { get; set; }
        public string Status { get; set; }
        public string UserType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
