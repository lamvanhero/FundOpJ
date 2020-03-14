using OppJar.Web.CustomValidations;

namespace OppJar.Web.Models
{
    public class PublicPageViewModel
    {
        [ValidateObject]
        public DonationViewModel Donation { get; set; }
        public EventViewModel Event { get; set; }
        public bool IsEdit { get; set; }
        public string ReturnUrl { get; set; }
    }
}
