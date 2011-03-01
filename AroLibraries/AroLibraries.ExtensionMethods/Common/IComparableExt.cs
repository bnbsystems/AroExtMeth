using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Common
{
    public static class IComparableExt
    {
        public static bool LessThan<T>(this IComparable<T> iComparable, T other)
        {
            return iComparable.CompareTo(other) < 0;
        }

        public static bool MoreThan<T>(this IComparable<T> iComparable, T other)
        {
            return iComparable.CompareTo(other) > 0;
        }

        public static bool ValueEquals<T>(this IComparable<T> iComparable, T other)
        {
            return iComparable.CompareTo(other) == 0;
        }
    }
}