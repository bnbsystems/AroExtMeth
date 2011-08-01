using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Data
{
    public static class BinaryExt
    {
        public static Image ToImage(this System.Data.Linq.Binary iBinary)
        {
            var arrayBinary = iBinary.ToArray();
            MemoryStream ms = new MemoryStream(arrayBinary);
            Image rImage = Image.FromStream(ms);
            return rImage;
        }
    }
}