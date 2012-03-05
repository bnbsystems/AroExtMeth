using System;
using System.Collections.Generic;
using System.Reflection;

namespace AroLibraries.ExtensionMethods
{
    public static class TypeExt
    {
        public static IEnumerable<PropertyInfo> GetReadAndWriteProperties(this Type iType)
        {
            if (iType != null)
            {
                foreach (var item in iType.GetProperties())
                {
                    if (item.CanRead && item.CanWrite)
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}