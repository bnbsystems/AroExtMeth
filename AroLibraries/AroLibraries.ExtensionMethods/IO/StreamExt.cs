using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.IO
{
    public static class StreamExt
    {
        public static bool SaveToFile(this Stream iStream, string iFilePath)
        {
            if (iStream != null && string.IsNullOrEmpty(iFilePath) == false)
            {
                using (FileStream fs = File.OpenWrite(iFilePath))
                {
                    const int blockSize = 1024;
                    byte[] buffer = new byte[blockSize];
                    int numBytes;
                    iStream.ReadTimeout = 50000;

                    while ((numBytes = iStream.Read(buffer, 0, blockSize)) > 0)
                    {
                        fs.Write(buffer, 0, numBytes);
                    }
                    fs.Close();
                    return true;
                }
            }
            return false;
        }

        public static bool CopyStreamTo(this Stream iStream, Stream output)
        {
            try
            {
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = iStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}