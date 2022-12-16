using System.Net.Mail;
using System.Text.RegularExpressions;

namespace API.Models
{
    public static class AccountHelper
    {
        public static bool IsValidEmail(this string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUsername(this string username)
        {
            var pattern = new Regex("^[0-9A-Za-z._-]*$");
            if (string.IsNullOrEmpty(username)
                || username.Length < 5
                || !pattern.IsMatch(username))
            {
                return false;
            }
            return true;
        }
    }
}
