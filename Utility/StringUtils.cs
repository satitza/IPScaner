using System;
using System.Text.RegularExpressions;

namespace IPScanner.Utility
{
    class StringUtils
    {
        public static bool IsValidEmail(string email)
        {
            if (email == "" || email == null)
            {
                return true;
            }
            else
            {
                try
                {
                    return Regex.IsMatch(email,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
        }

        public static bool IsNumberOnly(string number)
        {
            if (number == "" || number == null)
            {
                return true;
            }
            else
            {
                try
                {
                    return Regex.IsMatch(number, @"^[0-9]*$");
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
        }

        public static bool IsNumberAndCharacterOnly(string text)
        {
            if (text == "" || text == null)
            {
                return true;
            }
            else
            {
                try
                {
                    return Regex.IsMatch(text, @"^[a-zA-Z0-9_.-]*$");
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
        }
    }
}
