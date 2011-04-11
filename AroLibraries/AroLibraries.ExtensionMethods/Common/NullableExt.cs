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
    }
}