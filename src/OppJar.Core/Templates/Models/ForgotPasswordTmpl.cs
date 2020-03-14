using CSharpEmail.Models;

namespace OppJar.Core.EmailTemplates
{
    public class ForgotPasswordTmpl : TmplBase
    {
        public ForgotPasswordTmpl(string firstName, string lastName, string url) : this(typeof(ForgotPasswordTmpl).Name)
        {
            DisplayName = $"{firstName} {lastName}";
            RedirectUrl = url;
        }
        public ForgotPasswordTmpl(string tmplName) : base(tmplName)
        {
        }

        public string DisplayName { get; set; }
        public string RedirectUrl { get; set; }
    }
}
