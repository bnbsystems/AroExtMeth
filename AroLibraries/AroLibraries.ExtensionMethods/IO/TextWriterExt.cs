using System.Collections.Generic;
using System.IO;

namespace AroLibraries.ExtensionMethods.IO
{
    public static class TextWriterExt
    {
        public static void WriteLineList(this TextWriter iTextWriter, IEnumerable<string> iStrings)
        {
            if (iStrings != null)
            {
                foreach (var item in iStrings)
                {
                    iTextWriter.WriteLine(item);
                }
            }
        }
    }
}
