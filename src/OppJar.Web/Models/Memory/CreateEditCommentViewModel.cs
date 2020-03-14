using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class CreateEditCommentViewModel
    {
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
        [Required(ErrorMessage = "ReferId is required.")]
        public string ReferId { get; set; }
    }
}
