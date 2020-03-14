using OppJar.Common.Helpers;
using OppJar.Web.Exceptions;
using System;
using System.Net.Http;

namespace OppJar.Web.Helpers
{
    public static class RepsonseExtension
    {
        public static string GetErrors(this HttpResponseMessage responseMessage)
        {
            var json = responseMessage.Content.ReadAsStringAsync().Result;

            var badRequest = json.JsonToObj<ApiException>();

            return string.Join(Environment.NewLine, badRequest.Errors.Split(';'));
        }
    }
}
