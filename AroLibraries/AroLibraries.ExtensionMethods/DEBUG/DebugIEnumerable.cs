using System;
using System.Collections;
using System.Collections.Generic;

namespace AroLibraries.ExtensionMethods.DEBUG
{
    public static class DebugIEnumerable
    {
        public static Dictionary<int, IEnumerable<string>> Debug_PrintObject<T>(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                throw new NullReferenceException("Enumerator is NULL");
            }

            Type vType = typeof(T);
            var vProperties = vType.GetProperties();
            Dictionary<int, IEnumerable<string>> rDic = new Dictionary<int, IEnumerable<string>>();
            int objectNumber = 0;
            foreach (object item in enumerable)
            {
                T itemT = (T)item;
                IList<string> rObjectInfo = new List<string>();
                foreach (var properteItem in vProperties)
                {
                    var name = properteItem.Name;
                    var type = properteItem.PropertyType.Name;
                    var value = properteItem.GetValue(item, null);
                    string outputString = String.Format("Name:{0}\tValue:{1}\tType:{2}", name, value, type);
                    rObjectInfo.Add(outputString);
                }
                rDic.Add(objectNumber, rObjectInfo);
                objectNumber++;
            }
            return rDic;
        }

        public static IList<string> Debug_PrintObject<T>(this T iObject)
        {
            if (iObject == null)
            {
                throw new NullReferenceException("Enumerator is NULL");
            }
            IList<string> rObjectInfo = new List<string>();

            Type vType = typeof(T);
            var vProperties = vType.GetProperties();
            foreach (var item in vProperties)
            {
                var name = item.Name;
                var type = item.PropertyType.Name;
                var value = item.GetValue(iObject, null);
                string outputString = String.Format("Name:{0}\tValue:{1}\tType:{2}", name, value, type);
                rObjectInfo.Add(outputString);
            }
            return rObjectInfo;
        }
    }
}