using System;
using System.IO;
using System.Reflection;

namespace AroLibraries.ExtensionMethods.Common
{
    public static class AssemblyExt
    {
        public static DateTime GetLastWriteTime(this Assembly iAssembly)
        {
            if (ReferenceEquals(iAssembly, null))
            {
                throw new NullReferenceException("Assembly is NULL");
            }
            try
            {
                DateTime lastWriteDate = File.GetLastWriteTime(iAssembly.Location);
                return lastWriteDate;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}