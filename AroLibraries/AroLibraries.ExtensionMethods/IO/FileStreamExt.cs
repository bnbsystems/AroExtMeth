using System.IO;
using System.Linq;

namespace AroLibraries.ExtensionMethods.IO
{
    public static class FileStreamExt
    {
        public static FileStream ReopenFileStreamByName(this FileStream iFileStream)
        {
            if (iFileStream != null && File.Exists(iFileStream.Name))
            {
                return new FileStream(iFileStream.Name, FileMode.OpenOrCreate);
            }
            throw new IOException("Can't reopen FileStream");
        }

        public static void WriteBytesEmpty(this FileStream iFileStream, int counter)
        {
            var bytes = ByteExt.GenerateByteNew();
            var vEmptyByts = bytes.Take(counter).ToArray();
            iFileStream.Write(vEmptyByts, 0, counter);
        }

        public static void WriteBytesRandom(this FileStream iFileStream, int counter)
        {
            var bytes = ByteExt.GenerateByteRandom();
            var vEmptyByts = bytes.Take(counter).ToArray();
            iFileStream.Write(vEmptyByts, 0, counter);
        }

        public static FileAccess GetFileAccess(this FileStream iFileStream)
        {
            if (iFileStream != null)
            {
                var read = iFileStream.CanRead;
                var write = iFileStream.CanWrite;
                if (read && write)
                {
                    return FileAccess.ReadWrite;
                }
                else if (read)
                {
                    return FileAccess.Read;
                }
                else if (write)
                {
                    return FileAccess.Write;
                }
            }
            throw new IOException("FileStream is close");
        }
    }
}