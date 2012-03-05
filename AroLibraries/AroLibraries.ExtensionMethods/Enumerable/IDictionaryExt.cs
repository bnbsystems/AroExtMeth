using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class IDictionaryExt
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue result;
            dict.TryGetValue(key, out result);
            return result;
        }

        public static Hashtable ToHashtable<TValue>(this  IDictionary<string, TValue> dict)
        {
            Hashtable rHashtable = new Hashtable();
            if (dict != null)
            {
                foreach (var key in dict.Keys)
                {
                    var value = dict[key];
                    rHashtable.Add(key, value);
                }
            }
            return rHashtable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="http://jacobcarpenter.wordpress.com/2008/03/13/dictionary-to-anonymous-type/"/>
        /// <example>
        ///        var dict = new Dictionary<string, object> {
        ///    { "Name", "Jacob" },
        ///    { "Age", 26 },
        ///    { "FavoriteColors", new[] { ConsoleColor.Blue, ConsoleColor.Green } },
        ///    };
        ///    var person = dict.ToAnonymousType(
        ///     new
        ///    {
        ///    Name = default(string),
        ///    Age = default(int),
        ///    FavoriteColors = default(IEnumerable<ConsoleColor>),
        ///    Birthday = default(DateTime?),
        ///     });
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="anonymousPrototype"></param>
        /// <returns></returns>
        public static T ToAnonymousType<T, TValue>(this IDictionary<string, TValue> dict, T anonymousPrototype)
        {
            // get the sole constructor
            var ctor = anonymousPrototype.GetType().GetConstructors().Single();
            // conveniently named constructor parameters make this all possible...
            var args = from p in ctor.GetParameters()
                       let val = dict.GetValueOrDefault(p.Name)
                       select val != null && p.ParameterType.IsAssignableFrom(val.GetType()) ? (object)val : null;

            return (T)ctor.Invoke(args.ToArray());
        }

        public static IDictionary<TKey, TValue> TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> iDictionary, TKey iKey, TValue iValue)
        {
            if (iDictionary != null && iDictionary.ContainsKey(iKey) == false)
            {
                iDictionary.Add(iKey, iValue);
            }
            return iDictionary;
        }


        //todo: IDictionaryExt | Check this code 
        public static T[,] ToMatrix<T>(this IDictionary<Point, T> iDictionaryElementPoint)
        {
            if (iDictionaryElementPoint != null)
            {
                var vMaxX = iDictionaryElementPoint.Select(x => x.Key.X).Max();
                var vMaxY = iDictionaryElementPoint.Select(x => x.Key.Y).Max();
                var rMatrix = new T[vMaxX, vMaxY];
                foreach (var item in iDictionaryElementPoint)
                {
                    rMatrix[item.Key.X, item.Key.Y] = item.Value;
                }
                return rMatrix;
            }

            return new T[1, 1]{{default(T)}};
            
        }

        //todo: IDictionaryExt | Check this code 
        public static T[] ToVector<T>(this IDictionary<int, T> iDictionaryElementPoint)
        {
            if (iDictionaryElementPoint != null && iDictionaryElementPoint.Any() 
                && iDictionaryElementPoint.Select(x=>x.Key).All(x=>x> -1))
            {
                var vMax = iDictionaryElementPoint.Select(x => x.Key).Max();
                var rVector = new T[vMax];
                foreach (var item in iDictionaryElementPoint)
                {
                    rVector[item.Key] = item.Value;
                }
                return rVector;
            }
            return new T[1] {  default(T)  };
        }
    }
}