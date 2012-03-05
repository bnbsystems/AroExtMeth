using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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



        public static bool OpenBytesAsFile(this byte[] iBytes, string iExtension)
        {
            try
            {
                string tempFileName = "_tempOpenBytesAsFile." + iExtension;
                using (var fStream = new FileStream(tempFileName, FileMode.OpenOrCreate))
                {
                    fStream.Write(iBytes, 0, iBytes.Length);
                    fStream.Close();
                    System.Diagnostics.ProcessStartInfo PDFstartInfo = new System.Diagnostics.ProcessStartInfo(tempFileName);
                    System.Diagnostics.Process.Start(PDFstartInfo);
                }
                return true;
            }
            catch
            { }
            return false;
        }




        public static IEnumerable<Byte> GenerateByteNew()
        {
            while (true)
            {
                yield return new Byte();
            }
        }

        public static IEnumerable<Byte> GenerateByteRandom()
        {
            Random vrandom = new Random();
            while (true)
            {
                int number = vrandom.Next(0, 255);
                byte rByte = (byte)number;
                yield return rByte;
            }
        }

    }
}