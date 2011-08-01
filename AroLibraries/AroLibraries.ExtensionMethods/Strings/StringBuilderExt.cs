using System.Text;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class StringBuilderExt
    {
        public static bool Dispose(this StringBuilder iStringBuilder)
        {
            try
            {
                iStringBuilder.Length = 0;
                return true;
            }
            catch { }
            return false;
        }
    }
}