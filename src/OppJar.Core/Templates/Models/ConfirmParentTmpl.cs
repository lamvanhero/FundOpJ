using CSharpEmail.Models;

namespace OppJar.Core.EmailTemplates
{
    public class ConfirmParentTmpl : TmplBase
    {
        public ConfirmParentTmpl(string firstName, string lastName, string url) : this(typeof(ConfirmParentTmpl).Name)
        {
            DisplayName = $"{firstName} {lastName}";
            RedirectUrl = url;
        }
        public ConfirmParentTmpl(string tmplName) : base(tmplName)
        {
        }

        public string DisplayName { get; set; }
        public string RedirectUrl { get; set; }
    }
}
