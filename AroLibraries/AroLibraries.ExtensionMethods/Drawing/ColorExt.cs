using System;
using System.Drawing;
using AroLibraries.ExtensionMethods.Objects;

namespace AroLibraries.ExtensionMethods.Drawing
{
    public static class ColorExt
    {
        public static Color GetColorRandom(bool iUseAlpha)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int vAlpha = 255;
            if (!iUseAlpha)
            {
                vAlpha = random.Next(255);
            }
            int vRed = random.Next(255);
            int vGreen = random.Next(255);
            int vBlue = random.Next(255);
            Color vColor = Color.FromArgb(vAlpha, vRed, vGreen, vBlue);

            return vColor;
        }

        public static Color AddColor(this Color iColor, Color iColorAdded)
        {
            int alfa = (iColor.A + iColorAdded.A).GetInRangeValue(0, 255, 0, 255);
            int red = (iColor.R + iColorAdded.R).GetInRangeValue(0, 255, 0, 255);
            int green = (iColor.G + iColorAdded.G).GetInRangeValue(0, 255, 0, 255);
            int blue = (iColor.B + iColorAdded.B).GetInRangeValue(0, 255, 0, 255);
            Color vColor = Color.FromArgb(alfa, red, green, blue);
            return vColor;
        }

        public static Color GetRevorce(this Color iColor)
        {
            int red = 255 - iColor.R;
            int green = 255 - iColor.G;
            int blue = 255 - iColor.B;
            return Color.FromArgb(red, green, blue);
        }

        public static Color ToColorFromHex(string iStirngHex)
        {
            return System.Drawing.ColorTranslator.FromHtml(iStirngHex);
        }

        public static string ToHex(Color iColor)
        {
            return System.Drawing.ColorTranslator.ToHtml(iColor);
        }

        public static double GetDistance(this Color iColorTo, Color iColorFrom, Func<Color,Color,double> iDistanceFunc )
        {
            return iDistanceFunc(iColorTo, iColorFrom);

        }
        public static double GetDistanceEuklides(this Color iColorTo, Color iColorFrom)
        {
            return GetDistance(iColorTo, iColorFrom, (c_1,c_2) =>
                                                        Math.Sqrt
                                                        (
                                                            Math.Pow(c_1.B - c_2.B , 2) +
                                                            Math.Pow(c_1.G - c_2.G , 2) +
                                                            Math.Pow(c_1.R - c_2.R , 2) 
                                                        )
                );

        }
        public static double GetDistanceMinimal(this Color iColorTo, Color iColorFrom)
        {
            return GetDistance(iColorTo, iColorFrom, (c_1, c_2) =>
                                                Math.Min
                                                (
                                                    Math.Abs(c_1.B - c_2.B),
                                                   Math.Min( 
                                                        Math.Abs(c_1.G - c_2.G),
                                                        Math.Abs(c_1.R - c_2.R)
                                                        )
                                                )
                );
        }
        public static double GetDistanceMaximum(this Color iColorTo, Color iColorFrom)
        {
            return GetDistance(iColorTo, iColorFrom, (c_1, c_2) =>
                                                Math.Max
                                                (
                                                    Math.Abs(c_1.B - c_2.B),
                                                    Math.Max(
                                                        Math.Abs(c_1.G - c_2.G),
                                                        Math.Abs(c_1.R - c_2.R)
                                                        )
                                                )
                );
        }


    }
}