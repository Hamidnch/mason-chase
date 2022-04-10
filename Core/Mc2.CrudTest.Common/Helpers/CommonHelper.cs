using System.Globalization;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace Mc2.CrudTest.Common.Helpers
{
    public static class CommonHelper
    {
        // Examines the domain part of the email and normalizes it.
        public static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            IdnMapping idn = new();

            // Pull out and process domain name (throws ArgumentException on invalid)
            string domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }

        public static bool IsValidMobileNumber(this string phoneNumber)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber p = phoneNumberUtil.Parse(phoneNumber, null);
            bool isMobile = false;
            bool isValidNumber = phoneNumberUtil.IsValidNumber(p);
            PhoneNumberType numberType = phoneNumberUtil.GetNumberType(p);

            string phoneNumberType = numberType.ToString();

            if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
            {
                isMobile = true;
            }
            return isValidNumber && isMobile;
        }
    }
}