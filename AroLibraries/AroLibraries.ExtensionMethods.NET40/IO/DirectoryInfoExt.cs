using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;

namespace AroLibraries.ExtensionMethods.NET40.IO
{
    public static class DirectoryInfoExt
    {
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
