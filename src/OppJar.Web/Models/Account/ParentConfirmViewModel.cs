using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class ParentConfirmViewModel
    {
        [Required(ErrorMessage = "id is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "token is required.")]
        public string Token { get; set; }
    }

}
