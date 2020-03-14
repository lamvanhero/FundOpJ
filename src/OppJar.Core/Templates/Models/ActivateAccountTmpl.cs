using CSharpEmail.Models;

namespace OppJar.Core.EmailTemplates
{
    public class ActivateAccountTmpl : TmplBase
    {
        public ActivateAccountTmpl(string firstName, string lastName, string url) : this(typeof(ActivateAccountTmpl).Name)
        {
            DisplayName = $"{firstName} {lastName}";
            RedirectUrl = url;
        }
        public ActivateAccountTmpl(string tmplName) : base(tmplName)
        {
        }

        public string DisplayName { get; set; }
        public string RedirectUrl { get; set; }
    }
}
