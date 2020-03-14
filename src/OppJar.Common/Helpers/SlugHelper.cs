using System;
using System.Text.RegularExpressions;

namespace OppJar.Common.Helpers
{
    public class SlugHelper
    {
        public static string GenerateSlug(string baseName)
        {
            Random r = new Random();
            Regex reg = new Regex("[*'\",._&#^@]");
            baseName = reg.Replace(baseName, string.Empty);

            Regex reg1 = new Regex("[ ]");
            baseName = reg.Replace(baseName, "");

            return string.Format("{0}{1}", baseName.Replace(" ", string.Empty), r.Next(1000000));
        }
    }
}
