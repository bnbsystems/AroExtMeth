using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data.Linq;
using System.IO;
namespace AroLibraries.ExtensionMethods.Data
{
    public static class BinaryExt
    {
        public static Image ToImage(this System.Data.Linq.Binary iBinary)
        {
            var arrayBinary = iBinary.ToArray();
            Image rImage = null;

            using (MemoryStream ms = new MemoryStream(arrayBinary))
            {
                rImage = Image.FromStream(ms);
            }
            return rImage;
        }


    }
}
