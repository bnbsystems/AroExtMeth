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
            return me.CompareTo(lower) >= 0 && me.CompareTo(upper) <= 0;
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

        public static TIn ToIf<TIn>(this TIn iObject, Predicate<TIn> predicate, Func<TIn, TIn> func)
        {
            if (ReferenceEquals(iObject, null))
            {
                return default(TIn);
            }
            if (predicate(iObject))
            {
                return func(iObject);
            }
            return iObject;
        }

        public static void DoIfNotNull<T>(this T iObject, Action<T> action)
        {
            if (iObject != null)
            {
                action(iObject);
            }
        }

        public static int ToInt(this object iObject)
        {
            return ToInt(iObject, 0);
        }

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
                try
                {
                    rInt = Convert.ToInt32(iObject);
                    return rInt;
                }
                catch (Exception) //FormatException OverflowException
                {
                }
                return iDefaultvalue;
            }
            return rInt;
        }

        public static double ToDouble(this object iObject)
        {
            return ToDouble(iObject, 0D);
        }

        public static Double ToDouble(this object iObject, double iDefaultvalue)
        {
            if (iObject == null)
            {
                return iDefaultvalue;
            }

            double rDouble;
            bool isOk = double.TryParse(iObject.ToString(), out rDouble);
            if (isOk == false)
            {
                try
                {
                    rDouble = Convert.ToInt32(iObject);
                }
                catch (Exception) //FormatException OverflowException
                {
                }
                return iDefaultvalue;
            }
            return rDouble;
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

        public static bool ToBool(this object iObject)
        {
            bool rBool;
            if (iObject == null)
            {
                return false;
            }
            bool isOk = bool.TryParse(iObject.ToString(), out rBool);
            if (isOk == false)
            {
                try
                {
                    rBool = Convert.ToBoolean(iObject);
                }
                catch { }
            }
            return rBool;
        }

        public static DateTime ToDateTime(this object iObject, DateTime defaultDateTime)
        {
            if (ReferenceEquals(iObject, null))
            {
                return defaultDateTime;
            }
            DateTime rDateTime = defaultDateTime;
            if (DateTime.TryParse(iObject.ToString(), out rDateTime) == false)
            {
                try
                {
                    rDateTime = Convert.ToDateTime(iObject);
                }
                catch
                {
                    rDateTime = defaultDateTime;
                }
            }
            return rDateTime;
        }

        public static DateTime ToDateTime(this object iObject)
        {
            return ToDateTime(iObject, DateTime.Now);
        }

        public static DateTime? ToDateTimeNullable(this object iObject)
        {
            return ToDateTimeNullable(iObject, x => true);
        }

        public static DateTime? ToDateTimeNullable(this object iObject, Predicate<DateTime> predicate)
        {
            if (ReferenceEquals(iObject, null))
            {
                return null;
            }
            DateTime rDateTime;
            if (DateTime.TryParse(iObject.ToString(), out rDateTime))
            {
                try
                {
                    rDateTime = Convert.ToDateTime(iObject);
                }
                catch
                { }

                if (predicate(rDateTime))
                {
                    return rDateTime;
                }
            }
            return null;
        }

        public static string ToString<T>(this T iObject, Predicate<T> predicate, string iElseString)
        {
            if (ReferenceEquals(iObject, null))
            {
                return iElseString;
            }
            if (predicate(iObject))
            {
                return iObject.ToString();
            }
            return iElseString;
        }

        public static string ToStringNull(this object iObject)
        {
            if (ReferenceEquals(iObject, null))
                return "NULL";
            return iObject.ToString();
        }

        public static string ToStringLower(this object iobject)
        {
            return iobject.ToString().ToLower();
        }

        public static List<T> ToListCollection<T>(this T iObject)
        {
            List<T> gList = new List<T> { iObject };
            return gList;
        }

        #endregion Convert TO

        #region IS

        public static bool IsNotNull(this object iObject)
        {
            return (iObject != null);
        }

        public static bool IsNullOrDbnull(this object iObject)
        {
            return (iObject == null || iObject == DBNull.Value);
        }

        public static bool IsOneOf<T>(this T iObject, IEnumerable<T> enumerable)
        {
            return enumerable.Any(x => x.Equals(iObject));
        }

        #endregion IS

        //todo: test it
        public static IDictionary<string, string> GetPropertiesAndValuesString<T>(this T iObject)
        {
            IDictionary<string, string> vPropertiesAndValuesString = new Dictionary<string, string>();
            foreach (var prop in iObject.GetType().GetProperties())
            {
                var value = prop.GetValue(iObject, null);
                if (value == null)
                {
                    value = "NULL";
                }
                vPropertiesAndValuesString.Add(prop.Name, value.ToString());
            }
            return vPropertiesAndValuesString;
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

        public static IEnumerable<T> WhereObject<T>(this T iObject, Predicate<T> predicate)
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

        public static T IfNullGet<T>(this T iT, T iT2)
        {
            if (ReferenceEquals(iT, null))
            {
                return iT2;
            }
            return iT;
        }
    }
}