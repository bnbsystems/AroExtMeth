using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Objects
{
    public static class ObjectExt
    {
        public static bool In<T>(this T value, IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException("values is NULL");

            return values.Contains(value);
        }

        public static T Limit<T>(this T value, T maximum)
    where T : IComparable<T>
        {
            return value.CompareTo(maximum) < 1 ? value : maximum;
        }

        public static bool IsBetween<T>(this T me, T lower, T upper)
    where T : IComparable<T>
        {
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) < 0;
        }

        public static T GetInRangeValue<T>(this T me, T lower, T upper, T defaultIfLower, T defaultIfUpper)
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

        public static T GetInRangeValue<T>(this T me, T lower, T upper)
            where T : IComparable<T>
        {
            return GetInRangeValue<T>(me, lower, upper, lower, upper);
        }

        #region Convert TO

        public static int ToInt(this object iObject, int iDefaultvalue)
        {
            if (iObject == null)
            {
                return iDefaultvalue;
            }

            int rInt;
            bool isOk = int.TryParse(iObject.ToString(), out rInt);
            if (isOk == false)
            {
                return iDefaultvalue;
            }
            return rInt;
        }

        public static string ToStringByProperty<T>(this T iObject)
        {
            StringBuilder rStirng = new StringBuilder();
            var vProperties = iObject.GetType().GetProperties();
            foreach (var vProperteIter in vProperties)
            {
                string vStr = vProperteIter.Name + ":" + vProperteIter.GetValue(iObject, null) + "\t";
                rStirng.Append(vStr);
            }

            return rStirng.ToString();
        }

        #endregion Convert TO

        public static bool IsNotNull(this object iObject)
        {
            return (iObject != null);
        }

        public static bool IsNullOrDbnull(this object iObject)
        {
            return (iObject == null || iObject == DBNull.Value);
        }

        public static void CloneProperties<T1, T2>(this T1 origin, T2 destination)
        {
            // Instantiate if necessary
            if (destination == null) throw new ArgumentNullException("destination", "Destination object must first be instantiated.");
            // Loop through each property in the destination
            foreach (var destinationProperty in destination.GetType().GetProperties())
            {
                // find and set val if we can find a matching property name and matching type in the origin with the origin's value
                if (origin != null && destinationProperty.CanWrite)
                {
                    origin.GetType().GetProperties()
                        .Where(x => x.CanRead && (x.Name == destinationProperty.Name && x.PropertyType == destinationProperty.PropertyType))
                        .ToList()
                        .ForEach(x => destinationProperty.SetValue(destination, x.GetValue(origin, null), null));
                }
            }
        }

        #region Enumerable

        public static IEnumerable<TResult> SelectParent<TResult>(this TResult iObject, Func<TResult, TResult> selectParentFunc, bool yieldFirstElement)
        {
            if (yieldFirstElement)
            {
                yield return iObject;
            }
            var vParent = selectParentFunc(iObject);
            while (vParent != null)
            {
                yield return vParent;
                vParent = selectParentFunc(vParent);
            }
        }

        public static IEnumerable<T> Where<T>(this T iObject, Predicate<T> predicate)
        {
            if (predicate(iObject))
            {
                yield return iObject;
            }
        }

        public static IEnumerable<T> YieldSame<T>(this T item)
        {
            yield return item;
        }

        #endregion Enumerable
    }
}