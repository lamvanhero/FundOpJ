using System.Collections.Generic;
using System.Linq;

namespace OppJar.Core.Settings
{
    public class UserRoles
    {
        public static readonly string SuperAdministrator = "SuperAdministrator";
        public static readonly string Administrator = "Administrator";
        public static readonly string Parent = "Parent";
        public static readonly string Giver = "Giver";
        public static readonly string Child = "Child";

        public static IEnumerable<string> FindAll()
        {
            yield return SuperAdministrator;
            yield return Administrator;
            yield return Parent;
            yield return Giver;
            yield return Child;
        }

        public static bool AllowAccess(string userRole, string allowRole)
        {
            return userRole.Split(",").Any(role => role.Equals(allowRole));
        }
    }
}
