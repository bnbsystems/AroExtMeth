using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Drawing
{
    public static class BitmapExt
    {
        public static Bitmap Fill(this Bitmap iBitmap, Color iFillColor)
        {
            Bitmap rBitmap = new Bitmap(iBitmap);
            for (int xIterator = 0; xIterator < rBitmap.Width; xIterator++)
            {
                for (int yIterator = 0; yIterator < rBitmap.Height; yIterator++)
                {
                    rBitmap.SetPixel(xIterator, yIterator, iFillColor);
                }
            }
            return rBitmap;
        }

        public static Bitmap GenerateBitmap(int iWidth, int iHight, Color iColor)
        {
            Bitmap rBitmap = new Bitmap(iWidth, iWidth);
            return rBitmap.Fill(iColor);
        }
    }
}