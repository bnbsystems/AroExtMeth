using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class DecimalExt
    {
        public static float ToFloat(this decimal iDecimal)
        {
            float vFloat = float.NaN;
            float.TryParse(iDecimal.ToString(), out vFloat);
            return vFloat;
        }
    }
}