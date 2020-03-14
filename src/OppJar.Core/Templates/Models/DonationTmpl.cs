using CSharpEmail.Models;

namespace OppJar.Core.EmailTemplates
{
    public class DonationTmpl : TmplBase
    {
        public DonationTmpl(string senderName, string recipientName, string amount) : this(typeof(DonationTmpl).Name)
        {
            SenderName = senderName;
            RecipientName = RecipientName;
            Amount = amount;
        }
        public DonationTmpl(string tmplName) : base(tmplName)
        {
        }

        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string Amount { get; set; }
    }
}
