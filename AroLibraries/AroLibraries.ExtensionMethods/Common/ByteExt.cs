using System;
using System.Text;

namespace AroLibraries.ExtensionMethods.Common
{
    public static class ByteExt
    {
        public static string ToHex(this byte[] iBytes)
        {
            StringBuilder hexString = new StringBuilder(iBytes.Length);
            for (int i = 0; i < iBytes.Length; i++)
            {
                hexString.Append(iBytes[i].ToString("X2"));
            }
            return hexString.ToString();
        }

        public static string ToBase64(this byte[] iBytes)
        {
            return Convert.ToBase64String(iBytes);
        }
    }
}