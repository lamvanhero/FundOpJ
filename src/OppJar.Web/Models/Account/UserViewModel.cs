using System;
using System.ComponentModel.DataAnnotations;
using OppJar.Common.Enum;

namespace OppJar.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Avatar { get; set; } = "/assets/images/no_image.png";
        public string Banner { get; set; }
        [Required]
        public string Location { get; set; }
        public int Age { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public UserType PageType { get; set; }
        public string Slug { get; set; }
        public string SponsorId { get; set; }
        public string RecipientId { get; set; }
        public UserStatus Status { get; internal set; }
    }
}
