using System;

namespace AroLibraries.ExtensionMethods
{
    public static class NullableExt
    {
        public static bool HasValueAndEquals<T>(this Nullable<T> source, T target)
    where T : struct
        {
            return source.HasValue && source.Value.Equals(target);
        }

        public static bool HasValueAndEquals<T>(this Nullable<T> source, Nullable<T> target)
          where T : struct
        {
            return source.HasValue && source.Value.Equals(target);
        }

        public static T  ToValue<T>(this Nullable<T> source)
            where T : struct
        {
            return source.Value;

        }

        public static object ToDBValue<T>(this Nullable<T> source)
                    where T : struct
        {
            if (source.HasValue)
            {
                return source.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

    }
}