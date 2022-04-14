using Mc2.CrudTest.Common.Helpers;
using System.Net.Mail;
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
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return MailAddress.TryCreate(email, out _);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}