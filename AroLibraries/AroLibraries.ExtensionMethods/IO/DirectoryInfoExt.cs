using System;
using System.Collections.Generic;
using System.IO;
using System.Management;


namespace AroLibraries.ExtensionMethods.IO
{
    public static class DirectoryInfoExt
    {
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo iDirectoryInfo, Predicate<FileInfo> predicate)
        {
            foreach (var file in iDirectoryInfo.GetFiles())
            {
                if (predicate(file))
                {
                    yield return file;
                }
            }
        }

        public static bool Goto(this DirectoryInfo iDirectoryInfo, string path, out DirectoryInfo outDirectoryInfo)
        {
            outDirectoryInfo = iDirectoryInfo;
            string newDirString = Path.Combine(iDirectoryInfo.FullName, path);
            if (Directory.Exists(newDirString))
            {
                outDirectoryInfo = new DirectoryInfo(newDirString);
                return true;
            }
            return false;
        }

        public static long GetDirectorySize(this DirectoryInfo iDirectoryInfo)
        {
            long total = 0;

            foreach (System.IO.FileInfo file in iDirectoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                total += file.Length;
            }
            return total;
        }

    }
}