using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class CreateEditViewModel
    {
        [Required]
        public string Content { get; set; }
        public string FileUrl { get; set; }
        public string ParentId { get; set; }
        [Required]
        public string ReferId { get; set; }
    }
}
