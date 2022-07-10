using System;
using System.Text.RegularExpressions;

namespace GoodWorkersMVP.Helpers
{
    public static class RegexUtilities
    {
        public static bool isValidEmail(string email)
        {
            var expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
