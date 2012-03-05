using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using AroLibraries.ExtensionMethods;
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

        #region Select Pixels
        public static Bitmap SelectPixels(this Bitmap iBitmap, IEnumerable<Point> iPoints, Color iSelectionColor)
        {
            Bitmap rBitmap= new Bitmap(iBitmap);
            if (iPoints != null)
            {
                foreach (var pointIter in iPoints)
                {
                    rBitmap.SetPixel(pointIter.X, pointIter.Y, iSelectionColor);
                }
            }
            return rBitmap;
        }
        public static Bitmap SelectPixels(this Bitmap iBitmap, Rectangle iRectangle, Color iSelectionColor, bool innerFill)
        {
            Bitmap rBitmap = new Bitmap(iBitmap);
            for (int x = iRectangle.Left; x <= iRectangle.Right; x++)
            {
                for (int y = iRectangle.Top; y <= iRectangle.Bottom; y++)
                {
                     if(rBitmap.IsPixelInImage(x,y))
                    {
                    if (innerFill == false && 
                        (y == iRectangle.Top || x == iRectangle.Left ||
                        y == iRectangle.Bottom || x == iRectangle.Right ))
                    {

                        rBitmap.SetPixel(x, y, iSelectionColor);
                    }
                         else if(innerFill)
                    {
                     rBitmap.SetPixel(x, y, iSelectionColor);
                    }

                    }
                }

            }
            return rBitmap;
        }
        public static Bitmap SelectPixels(this Bitmap iBitmap, Rectangle iRectangle, Color iSelectionColor)
        {
            return SelectPixels(iBitmap, iRectangle, iSelectionColor, false);
        }
        #endregion

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

        public static Bitmap GenerateBitmap(int iWidth, int iHight, Color iColor)
        {
            Bitmap rBitmap = new Bitmap(iWidth, iHight);
            return rBitmap.Fill(iColor);
        }

        public static Point GetCenterPointOfObject(this Bitmap iBitmap)
        {
            var points = iBitmap.GetPoints().Where(x=>iBitmap.GetPixel(x).A> 0 );
            int pointCounter = points.Count();
            var pointsX = points.Select(x=>x.X);
            var pointsY = points.Select(x=>x.Y);
            var sumPointX = pointsX.Sum();
            var sumPointY = pointsY.Sum();

            int iw2 = sumPointX / pointCounter;
            int ih2 = sumPointY / pointCounter;
            
            return new Point(iw2,ih2);
        }
        public static Point GetCenterPointOfImage(this Bitmap iBitmap)
        {
            int ih2 = iBitmap.Height / 2;
            int iw2 = iBitmap.Width / 2;
            return new Point(iw2, ih2);
        }

        #region Is Pixel in image
        public static bool IsPixelInImage(this Bitmap iBitmap, int iPxelWight, int iPixelHight, Predicate<Color> iColorPredicate)
        {
             if(iBitmap == null)
            {
                return false;
            }
            if (iPixelHight < 0 || iPxelWight < 0)
            {
                return false;
            }

            if (iBitmap.Height <= iPixelHight || iBitmap.Width <= iPxelWight)
            {
                return false;
            }
            if (iColorPredicate(iBitmap.GetPixel(iPxelWight, iPixelHight))== false)
            {
                return false;
            }
            return true;
        }
        public static bool IsPixelInImage(this Bitmap iBitmap, int iPxelWight, int iPixelHight)
        {
            return IsPixelInImage(iBitmap, iPxelWight, iPixelHight, x=>true);
        }
        public static bool IsPixelInImage(this Bitmap iBitmap, int iPxelWight, int iPixelHight, Color iColor)
        {
            return IsPixelInImage(iBitmap, iPxelWight, iPixelHight, x => x == iColor);
        }
        #endregion

        public static bool IsPointInImage(this Bitmap iBitmap, Point iPoint)
        {
            if(iPoint.X >= 0 && iPoint.Y >= 0)
            {
                if(iBitmap!= null)
                {
                    if(iBitmap.Width > iPoint.X && iBitmap.Height > iPoint.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Bitmap SeparationY(this Bitmap iBitmap, int from, int to)
        {
            Bitmap rBitmap = new Bitmap(iBitmap.Width, to - from);
            for (int x = 0; x < iBitmap.Width; x++)
            {
                for (int y = from; y < to; y++)
                {
                    rBitmap.SetPixel(x, y-from, iBitmap.GetPixel(x, y));
                }
            }
            return rBitmap;
        }

        //todo: BitmapExt | SeperationX 



        public static Bitmap AddImageToSide(this Bitmap iFirstBitmap, Bitmap iSecandBitmap, int iDistance)
        {
            int vDistance = Math.Max(iDistance, 0);
            int vFirstImageWidthWithDistance = iFirstBitmap.Width + vDistance;
            int vMaxHeight = Math.Max(iFirstBitmap.Height, iSecandBitmap.Height);
            Bitmap rBitmap = new Bitmap(vFirstImageWidthWithDistance + iSecandBitmap.Width, vMaxHeight);
            for (int y = 0; y < iFirstBitmap.Height; y++)
            {
                for (int x = 0; x < iFirstBitmap.Width; x++)
                {
                    rBitmap.SetPixel(x, y, iFirstBitmap.GetPixel(x, y));
                }
            }

            for (int y = 0; y < iSecandBitmap.Height; y++)
            {
                for (int x = 0; x < iSecandBitmap.Width; x++)
                {
                    rBitmap.SetPixel(vFirstImageWidthWithDistance + x, y, iSecandBitmap.GetPixel(x, y));
                }
            }


            return rBitmap;
        }


        #region GetPixels and GetPonint
        public static Color GetPixel(this Bitmap iBitmap, Point iPoint)
        {
            return iBitmap.GetPixel(iPoint.X, iPoint.Y);
        }
        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap)
        {
            return GetPixels(iBitmap, true, x=>true);
        }

        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap, IEnumerable<Point> iPoints)
        {
            if (iPoints != null && iBitmap!= null)
            {
                foreach (var point in iPoints)
                {
                    if (iBitmap.IsPointInImage(point))
                    {
                        yield return iBitmap.GetPixel(point);
                    }
                }
            }
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

        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap, bool iGetHorizontal)
        {
            return GetPixels(iBitmap, iGetHorizontal, x => true);
        }
        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap, Predicate<Color> predicate)
        {
            return GetPixels(iBitmap, true, predicate);
        }
        public static IEnumerable<Point> GetPoints(this Bitmap iBitmap)
        {
            return GetPoints(iBitmap, true);
        }
        public static IEnumerable<Point> GetPoints(this Bitmap iBitmap, bool iGetHorizontal)
        {
            return GetPoints(iBitmap, iGetHorizontal, x => true);
        }
        public static IEnumerable<Point> GetPoints(this Bitmap iBitmap, bool iGetHorizontal, Predicate<Point> predicate)
        {
            if (iGetHorizontal)
            {
                for (int iHeight = 0; iHeight < iBitmap.Height; iHeight++)
                {
                    for (int iWidth = 0; iWidth < iBitmap.Width; iWidth++)
                    {
                        var point = new Point(iWidth, iHeight);
                        if (predicate(point))
                        {
                            yield return point;
                        }

                    }
                }
            }
            else
            {
                for (int iWidth = 0; iWidth < iBitmap.Width; iWidth++)
                {
                    for (int iHeight = 0; iHeight < iBitmap.Height; iHeight++)
                    {
                        var point = new Point(iWidth, iHeight);
                        if (predicate(point))
                        {
                            yield return point;
                        }
                    }
                }
            }
        
        }
        public static IEnumerable<Color> GetPixels(this Bitmap iBitmap,bool iGetHorizontal, Predicate<Color> predicate)
        {
            var points = GetPoints(iBitmap, iGetHorizontal);
            if (points != null)
            {
                foreach (var point in points)
                {
                    Color col = iBitmap.GetPixel(point.X, point.Y);
                    if (predicate(col))
                    {
                        yield return col;
                    }
                }
            }
        }
       
        
        public static IEnumerable<Point> GetPointsNeighborhood(this Bitmap iBitmap, Point centerPoint)
        {
            if(iBitmap!= null && iBitmap.IsPointInImage(centerPoint))
            {
                Point upperPoint = centerPoint.Add(0,-1);
                if(iBitmap.IsPointInImage(upperPoint))
                {
                    yield return upperPoint;
                }

                Point downPoint = centerPoint.Add(0,1);
                if(iBitmap.IsPointInImage(downPoint))
                {
                    yield return downPoint;
                }

                Point leftPoint = centerPoint.Add(-1,0);
                if(iBitmap.IsPointInImage(leftPoint))
                {
                    yield return leftPoint;
                }

                Point rightPoint = centerPoint.Add(1,0);
                if(iBitmap.IsPointInImage(rightPoint))
                {
                    yield return rightPoint;
                }

            }
        }
        #endregion

        #region To
        public static Bitmap ToImageBlackFromAlfa(this Bitmap bitmap)
        {
            Bitmap rBitmap = new Bitmap(bitmap);
            for (int h = 0; h < bitmap.Height; h++)
            {
                for (int w = 0; w < bitmap.Width; w++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.A < 255)
                    {
                        rBitmap.SetPixel(w, h, Color.Black);
                    }
                }
            }
            return rBitmap;
        }

        public static Bitmap ToImageSchemeWhiite(this Bitmap bitmap)
        {
            Bitmap rBitmap = new Bitmap(bitmap);
            for (int h = 0; h < bitmap.Height; h++)
            {
                for (int w = 0; w < bitmap.Width; w++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.B > 0 && color.G > 0 && color.R > 0)
                    {
                        rBitmap.SetPixel(w, h, Color.White);
                    }
                }
            }
            return rBitmap;
        }


        public static Binary ToBinary(this Bitmap iBitmap)
        {
            byte[] bytes = iBitmap.ToByte();
            Binary binary = new Binary(bytes);
            return binary;
        }

        public static byte[] ToByte(this Bitmap iBitmap)
        {
            byte[] rByte = new byte[0];
            using (MemoryStream ms = new MemoryStream())
            {
                iBitmap.Save(ms, ImageFormat.Bmp);
                rByte = ms.ToArray();
            }
            return rByte;
        }
        #endregion

        public static Color TryGetPixel(this Bitmap iBitmap, int w, int h, Color iDefaultColor)
        {
            if(w >= 0 && h>=0)
            {
                if(w < iBitmap.Width && h<iBitmap.Height)
                {
                    return iBitmap.GetPixel(w, h);
                }
            }
            return iDefaultColor;
        }
        public static Color TryGetPixel(this Bitmap iBitmap, int w, int h)
        {
            return TryGetPixel(iBitmap, w, h, Color.Empty);
        }

        public static Bitmap AddTextOnImage(this Bitmap iBItmap, Point iPoint, string iWord)
        {
            Font vfont = new Font("Arial", 9);
            Bitmap rBitmap = iBItmap.Clone(iBItmap.Size.ToRectangle(), iBItmap.PixelFormat);
            {
                using (Graphics g = Graphics.FromImage(rBitmap))
                {

                    var size = g.MeasureString(iWord, vfont);
                    g.DrawString(iWord, vfont, new SolidBrush(Color.Red), iPoint);
                    g.Save();
                }
                return rBitmap;
            }
        }

        public static Rectangle ToRectangle(this Bitmap iBitmap)
        {
            return new Rectangle(0,0,iBitmap.Width,iBitmap.Height);
        }
        public static Bitmap SetPixelFormat(this Bitmap iBitmap, PixelFormat newPixelFormat)
        {
            if (iBitmap.PixelFormat == newPixelFormat)
            {
                return iBitmap;
            }

            Rectangle rec = iBitmap.ToRectangle();
            return iBitmap.Clone(rec, newPixelFormat);

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


        public static Bitmap GetDifference(this Bitmap iBitmap, Bitmap iSecondBitmap)
        {
            Bitmap rBitmap = new Bitmap(iBitmap.Width, iBitmap.Height);
            if (iBitmap != null && iSecondBitmap != null)
            {
                for (int y = 0; y < iBitmap.Height; y++)
                {
                    for (int x = 0; x < iBitmap.Width; x++)
                    {
                        var colorFirst = iBitmap.GetPixel(x, y);
                        var colorSecond = iSecondBitmap.GetPixel(x, y);
                        int difference = ((int)colorFirst.GetDistanceEuklides(colorSecond)).InRange(0,255);
                        var colorDifference = Color.FromArgb(difference, difference, difference);

                        rBitmap.SetPixel(x, y, colorDifference);
                    }

                }

            }
            return rBitmap;
        }


        //todo: BitmapExt | Check this code 
        public static Dictionary<Point, double> GetDifferenceDictionary(this Bitmap iBitmap, Bitmap iSecondBitmap, Func<Color, Color, double> metricFunc)
        {
            Dictionary<Point, double> differenceDic = new Dictionary<Point, double>();
            for (int x = 0; x < iBitmap.Width; x++)
            {
                for (int y = 0; y < iBitmap.Height; y++)
                {
                    var vPoint = new Point(x, y);
                    var vColorFirst = iBitmap.GetPixel(vPoint);
                    var vColorSecond = iSecondBitmap.GetPixel(vPoint);
                    var vDifference = metricFunc(vColorFirst, vColorSecond);
                    differenceDic.Add(vPoint, vDifference);
                }
            }
            return differenceDic;
        }   

        public static double GetDiffecenceInPixels(this Bitmap iBitmap, Bitmap iSecondBitmap)
        {
            int Size = iBitmap.Width * iBitmap.Height;
            double differenceCounter = 0;
            for (int x = 0; x < iBitmap.Width; x++)
            {
                for (int y = 0; y < iBitmap.Height; y++)
                {
                    var vFirstPixel = iBitmap.GetPixel(x, y);
                    var vSecondPixel = iSecondBitmap.GetPixel(x, y);
                    if (vFirstPixel != vSecondPixel)
                    {
                        differenceCounter++;
                    }

                }
            }
            return differenceCounter / Size;
        }

        //public static Bitmap GetInnerBitmap(this Bitmap iBitmap, int x,int y, int wight, int hight)
        //{
        //    Bitmap rBitmap= new Bitmap(wight,hight);
        //    for (int iWidth = 0; iWidth < iBitmap.Width; iWidth++)
        //    {
        //        for (int iHeight = 0; iHeight < iBitmap.Height; iHeight++)
        //        {
        //            Color vColor = iBitmap.GetPixel(iWidth, iHeight);
        //            rBitmap.

        //        }
        //    }

        //    return rBitmap;
        //}

        //public static IEnumerable<Color> GetMedianColors(this Bitmap iBitmap, Predicate<Color> colorPredicate)
        //{
        //    var colors = iBitmap.GetPixels(colorPredicate);



        //}

        //public static Point GetCenterPointOfObject(this Bitmap iBitmap, Color iExceptColor)
        //{
        //    return GetCenterPointOfObject(iBitmap, x => x == iExceptColor);
        //}
        //public static Point GetCenterPointOfObject(this Bitmap iBitmap, Predicate<Color> exceptColor)
        //{
        //    for (int iHeight = 0; iHeight < iBitmap.Height; iHeight++)
        //    {
        //        for (int iWidth = 0; iWidth < iBitmap.Width; iWidth++)
        //        {
        //            iBitmap.GetPixel(iWidth, iHeight);

        //        }
        //    }

        //}

        //public static bool SaveJpeg(this Bitmap iBitmap, string fileName)
        //{
        //    return SaveJpeg(iBitmap, fileName, 100);
        //}

        //public static bool SaveJpeg(this Bitmap iBitmap, string iFileName, int iQuality)
        //{
        //    try
        //    {
        //        EncoderParameters encoderParameters = new EncoderParameters(1);
        //        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)iQuality);
        //        iBitmap.Save(iFileName, ImageFormat.Jpeg.GetEncoder(), encoderParameters);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return false;
        //}
    }
}