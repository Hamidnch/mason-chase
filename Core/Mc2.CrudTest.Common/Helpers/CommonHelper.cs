using PhoneNumbers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Common.Helpers
{
    public static class CommonHelper
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        #region Private Methods

        private static PhoneNumber ParseToPhoneNumber(this string phoneNumber)
        {
            return PhoneNumberUtil.Parse(phoneNumber, null);
        }

        private static bool IsValidMobileType(this PhoneNumber p)
        {
            PhoneNumberType numberType = PhoneNumberUtil.GetNumberType(p);
            return numberType == PhoneNumberType.MOBILE;
        }

        #endregion Private Methods


        #region Public Methods

        public static bool IsValidMobileNumber(this string phoneNumber)
        {
            PhoneNumber p = phoneNumber.ParseToPhoneNumber();

            bool isValidNumber = PhoneNumberUtil.IsValidNumber(p);

            bool isValidMobile = p.IsValidMobileType();

            return isValidNumber && isValidMobile;
        }

        // Examines the domain part of the email and normalizes it.
        public static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            IdnMapping idn = new();

            // Pull out and process domain name (throws ArgumentException on invalid)
            string domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }

        #endregion Public Methods
    }
}