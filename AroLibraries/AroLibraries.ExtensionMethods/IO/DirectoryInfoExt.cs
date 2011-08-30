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

        #region Sharing

        public static void Share(this DirectoryInfo iDirectoryInfo, string shareName, string description) //todo: test it
        {
            var managementClass = new ManagementClass("Win32_Share");

            ManagementBaseObject parameters = managementClass.GetMethodParameters("Create");

            parameters["Description"] = description;
            parameters["Name"] = shareName;
            parameters["Path"] = iDirectoryInfo.FullName;
            parameters["Type"] = 0;

            ManagementBaseObject outs = managementClass.InvokeMethod("Create", parameters, null);

            if (outs == null || ((uint)(outs.Properties["ReturnValue"].Value) != 0))
            {
                throw new IOException("Unable to share directory.");
            }
        }

        public static void Unshare(this DirectoryInfo iDirectoryInfo, string shareName) //todo: test it
        {
            var managementObject = new ManagementObject(string.Format("Win32_Share.Name='{0}'", shareName));
            try
            {
                managementObject.InvokeMethod("Delete", null, null);
            }
            catch (ManagementException ex)
            {
                throw new IOException("Unable to unshare folder", ex);
            }
        }

        #endregion Sharing
    }
}