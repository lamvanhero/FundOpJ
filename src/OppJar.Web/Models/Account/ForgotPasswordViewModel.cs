using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        public string RedirectUrl { get; set; }
    }

}
