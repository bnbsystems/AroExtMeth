using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using AroLibraries.ExtensionMethods.Objects;

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

        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap, int iWidth, int iHeight, int pixelSize)
        {
            for (int x = iWidth; x < iWidth + pixelSize && x < iBitmap.Width; x++)
            {
                for (int y = iHeight; y < iHeight + pixelSize && y < iBitmap.Height; y++)
                {
                    yield return iBitmap.GetPixel(x, y);
                }
            }
        }

        public static Bitmap Fill(this Bitmap iBitmap, Color iColor, int iWidth, int iHeight, int pixelSize)
        {
            Bitmap rBitmap = new Bitmap(iBitmap);
            for (int x = iWidth; x < iWidth + pixelSize && x < iBitmap.Width; x++)
            {
                for (int y = iHeight; y < iHeight + pixelSize && y < iBitmap.Height; y++)
                {
                    rBitmap.SetPixel(x, y, iColor);
                }
            }
            return rBitmap;
        }

        public static Bitmap ToPixellate(this Bitmap iBitmap, int iPixelSize)
        {
            if (iPixelSize < 2)
            {
                return iBitmap;
            }
            Bitmap rBitmap = new Bitmap(iBitmap.Width, iBitmap.Height);
            for (int x = 0; x < iBitmap.Width; x = iPixelSize + x)
            {
                for (int y = 0; y < iBitmap.Height; y = iPixelSize + y)
                {
                    var vPixels = GetPixels(iBitmap, x, y, iPixelSize);
                    var vPixelColorB = vPixels.Average(x_c => x_c.B).ToInt();
                    var vPixelColorG = vPixels.Average(x_c => x_c.G).ToInt();
                    var vPixelColorR = vPixels.Average(x_c => x_c.R).ToInt();
                    var vPixelColor = Color.FromArgb(vPixelColorR, vPixelColorG, vPixelColorB);
                    rBitmap = Fill(rBitmap, vPixelColor, x, y, iPixelSize);
                }
            }

            return rBitmap;
        }
    }
}