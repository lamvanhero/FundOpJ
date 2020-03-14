using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OppJar.Web.Models
{
    public class EventViewModel
    {
        public string Id { get; set; }
        [BindRequired]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Photo is required.")]
        public string UrlPhotos { get; set; }
        public string NiceUrl { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
