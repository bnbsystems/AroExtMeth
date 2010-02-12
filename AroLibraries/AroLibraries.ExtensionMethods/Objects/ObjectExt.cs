using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Objects
{
    public static class ObjectExt
    {
        public static bool Ext_In<T>(this T value, IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException("values is NULL");

            return values.Contains(value);
        }

        public static T Ext_Limit<T>(this T value, T maximum)
    where T : IComparable<T>
        {
            return value.CompareTo(maximum) < 1 ? value : maximum;
        }
        public static bool Ext_IsBetween<T>(this T me, T lower, T upper)
    where T : IComparable<T>
        {
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) < 0;
        }

        public static T Ext_GetInRangeValue<T>(this T me, T lower, T upper, T defaultIfLower, T defaultIfUpper)
    where T : IComparable<T>
        {
            if (me.CompareTo(lower) >= 0 && me.CompareTo(upper) <= 0)
            {
                return me;
            }
            else if (me.CompareTo(lower) < 0)
            {
                return defaultIfLower;
            }
            else if (me.CompareTo(upper) > 0)
            {
                return defaultIfUpper;
            }
            return default(T);
        }
        public static T Ext_GetInRangeValue<T>(this T me, T lower, T upper)
            where T : IComparable<T>
        {
            return Ext_GetInRangeValue<T>(me, lower, upper, lower, upper);
        }

    }
}
