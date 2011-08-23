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

        public static bool SaveJpeg(this Bitmap iBitmap, string fileName)
        {
            return SaveJpeg(iBitmap, fileName, 100);
        }

        public static bool SaveJpeg(this Bitmap iBitmap, string iFileName, int iQuality)
        {
            try
            {
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)iQuality);
                iBitmap.Save(iFileName, ImageFormat.Jpeg.GetEncoder(), encoderParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }


    }
}