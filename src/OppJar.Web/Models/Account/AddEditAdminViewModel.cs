using OppJar.Web.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class AddEditAdminViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }
        public ActionMode ActionMode { get; set; } = ActionMode.Create;
    }
}
