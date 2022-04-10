using Mc2.CrudTest.Common.Helpers;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Domain.Validators
{
    public static class EmailValidator
    {
        public static bool Validate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim().ToLower();

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$",
                    CommonHelper.DomainMapper,
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}