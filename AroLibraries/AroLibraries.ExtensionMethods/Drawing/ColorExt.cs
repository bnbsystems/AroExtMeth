using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
            int alfa = (iColor.A + iColorAdded.A).Ext_GetInRangeValue(0, 255, 0, 255);
            int red = (iColor.R + iColorAdded.R).Ext_GetInRangeValue(0, 255, 0, 255);
            int green = (iColor.G + iColorAdded.G).Ext_GetInRangeValue(0, 255, 0, 255);
            int blue = (iColor.B + iColorAdded.B).Ext_GetInRangeValue(0, 255, 0, 255);
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

        public static Color FromHex(string iStirngHex)
        {
            return System.Drawing.ColorTranslator.FromHtml(iStirngHex);
        }

        public static string ToHex(Color iColor)
        {
            return System.Drawing.ColorTranslator.ToHtml(iColor);
        }
    }
}