using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.IO
{
    public static class FileInfoExt
    {
        public static bool ExecFile(this FileInfo iFileinfo, string iArguments)//todo: test it
        {
            try
            {
                if (iFileinfo.Exists)
                {
                    Process ProcessObj = new Process();
                    ProcessObj.StartInfo.FileName = iFileinfo.FullName;
                    if (string.IsNullOrEmpty(iArguments) == false)
                    {
                        ProcessObj.StartInfo.Arguments = iArguments;
                    }
                    ProcessObj.StartInfo.UseShellExecute = false;
                    ProcessObj.StartInfo.CreateNoWindow = true;
                    ProcessObj.StartInfo.RedirectStandardOutput = true;
                    ProcessObj.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool RenameFileName(this FileInfo iFileInfo, string iNewFileName)
        {
            if (iFileInfo != null && string.IsNullOrEmpty(iNewFileName) == false)
            {
                if (iFileInfo.Exists)
                {
                    string vExtension = iFileInfo.Extension;
                    string vNewFileName = iNewFileName + vExtension;
                    string vNewFullName = iFileInfo.DirectoryName + vNewFileName;
                    try
                    {
                        iFileInfo.MoveTo(vNewFullName);
                        if (File.Exists(vNewFullName))
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return false;
        }

        public static bool RenameFileExtension(this FileInfo iFileInfo, string iNewFileName)//test it
        {
            if (iFileInfo != null && string.IsNullOrEmpty(iNewFileName) == false)
            {
                if (iFileInfo.Exists)
                {
                    string vNewExtension = iNewFileName;
                    if (iNewFileName[0] == '.')
                    {
                        vNewExtension = iNewFileName.Substring(1);
                    }
                    string vNewName = iFileInfo.Name.Replace(iFileInfo.Extension, "." + vNewExtension);
                    string vNewFullName = iFileInfo.DirectoryName + vNewName;
                    try
                    {
                        iFileInfo.MoveTo(vNewFullName);
                        if (File.Exists(vNewFullName))
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return false;
        }
    }
}