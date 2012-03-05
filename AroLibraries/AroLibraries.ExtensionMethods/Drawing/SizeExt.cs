using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AroLibraries.ExtensionMethods.Drawing
{
    public static class SizeExt
    {
        public static Rectangle ToRectangle(this Size iSize)
        {
            return new Rectangle(new Point(0, 0), iSize);
        }

    }
}
