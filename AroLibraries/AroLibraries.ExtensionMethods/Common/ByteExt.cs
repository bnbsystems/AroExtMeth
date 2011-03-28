using System;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class ByteExt
    {
        public static string ToHex(this byte[] iBytes)
        {
            return ToHex(iBytes, "");
        }

        public static string ToHex(this byte[] iBytes, string seperator)
        {
            StringBuilder hexString = new StringBuilder(iBytes.Length);
            bool wasAdded = false;
            for (int i = 0; i < iBytes.Length; i++)
            {
                if (wasAdded)
                {
                    hexString.Append(seperator);
                }
                hexString.Append(iBytes[i].ToString("X2"));
                wasAdded = true;
            }

            return hexString.ToString();
        }

        public static string ToBase64(this byte[] iBytes)
        {
            return Convert.ToBase64String(iBytes);
        }
    }
}