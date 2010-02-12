using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class IntExt
    {
        public static IEnumerable<int> Ext_IterateTo(this int start, int end)
        {
            var diff = end - start > 0 ? 1 : -1;
            for (var current = start; current != end; current += diff)
            {
                yield return current;
            }
        }

        public static bool Ext_ToBool(this int i)
        {
            if (i > 0)
                return true;
            return false;
        }

        public static bool Ext_IsEven(this int iInt)
        {
            if ((iInt % 2) == 0)
            {
                return true;
            }
            return false;
        }
        public static bool Ext_IsPositive(this int iInt)
        {
            if (iInt > 0)
                return true;
            return false;
        }

    }
}
