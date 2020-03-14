using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace OppJar.Common.Helpers
{
    public static class ClaimKeyHelper
    {
        public const string USER_ID = ClaimTypes.NameIdentifier;
        public const string EMAIL = ClaimTypes.Email;
        public const string NAME = ClaimTypes.Name;
        public const string ROLE = ClaimTypes.Role;
        public const string AVATAR = "Avatar";
        public const string CUSTOMER_ID = "CustomerId";
        public const string FIRST_NAME = "FirstName";
        public const string LAST_NAME = "LastName";

        public static string GetValue(this IPrincipal user, string key)
        {
            if (user == null)
            {
                return string.Empty;
            }

            var claim = ((ClaimsIdentity)user.Identity).Claims.FirstOrDefault(x => x.Type.Equals(key));

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }
    }
}
