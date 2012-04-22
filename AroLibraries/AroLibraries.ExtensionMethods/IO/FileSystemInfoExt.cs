using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.IO
{
    public static class FileSystemInfoExt
    {
        public static IEnumerable<string> GetFoldersName(this FileSystemInfo iFileSystemInfoExt)
        {
            var vFolders = iFileSystemInfoExt.FullName.Split(new char[] { '/', '\\' });
            return vFolders.Select(x => x.ToString()).ToList();
        }
    }
}