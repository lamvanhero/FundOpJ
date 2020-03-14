using CSharpEmail.Models;

namespace OppJar.Core.EmailTemplates
{
    public class DonationMailToParentTmpl : TmplBase
    {
        public DonationMailToParentTmpl(string displayName, string senderName, string recipientName, string amount) : this(typeof(DonationMailToParentTmpl).Name)
        {
            DisplayName = displayName;
            SenderName = senderName;
            RecipientName = recipientName;
            Amount = amount;
        }
        public DonationMailToParentTmpl(string tmplName) : base(tmplName)
        {
        }
        public string DisplayName { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string Amount { get; set; }
    }
}
