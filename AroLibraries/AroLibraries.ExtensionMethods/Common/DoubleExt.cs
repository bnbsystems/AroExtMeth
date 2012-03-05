using System;

namespace AroLibraries.ExtensionMethods
{
    public static class DoubleExt
    {
        public static double Round(this double iDouble, int positionSize)
        {
            return Math.Round(iDouble, positionSize);
        }
        
    }
}