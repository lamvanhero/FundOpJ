using System.ComponentModel.DataAnnotations;

namespace OppJar.Web.Models
{
    public class MyPageViewModel
    {
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        public string UserId { get; set; }
        [Display(Name = "")]
        public string AvatarUrl { get; set; }
        public string Slug { get; set; }
    }
}
