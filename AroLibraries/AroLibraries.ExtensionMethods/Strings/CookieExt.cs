using System;
using System.Linq;
using System.Net;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class CookieExt
    {
        public static Cookie FromString(string iCookeString)
        {
            if (ReferenceEquals(iCookeString, null) || iCookeString.Contains('=') == false)
            {
                return new Cookie();
            }

            try
            {
                string[] str = iCookeString.Trim().Split(new char[] { '=' });
                if (str.Any() && str.Count() == 2)
                {
                    return new Cookie(str[0], str[1]);
                }
            }
            catch (Exception)
            {
            }
            return new Cookie();
        }
    }
}