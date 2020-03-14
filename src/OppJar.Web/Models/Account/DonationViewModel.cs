using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class DonationViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlPhotos { get; set; }
        [Required(ErrorMessage = "Your name is required.")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "Your email is required.")]
        public string Email { get; set; }
        public string Message { get; set; }
        [Range(0, 500, ErrorMessage = "Amount should be greater than 0$ and less than 500$.")]
        public decimal Amount { get; set; }
    }
}
