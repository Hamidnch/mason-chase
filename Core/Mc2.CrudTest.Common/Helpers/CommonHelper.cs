using System.Globalization;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Common.Helpers
{
    public static class CommonHelper
    {
        // Examines the domain part of the email and normalizes it.
        public static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            IdnMapping idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            string domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }
    }
}