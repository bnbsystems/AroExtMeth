using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class IEnumerableExt
    {
        public static T Ext_Aggregate<T>(this IEnumerable<T> list, Func<T, T, T> aggregateFunction)
        {
            return Ext_Aggregate<T>(list, default(T), aggregateFunction);
        }
        public static T Ext_Aggregate<T>(this IEnumerable<T> list, T defaultValue, Func<T, T, T> aggregateFunction)
        {
            return list.Count() <= 0 ?
                defaultValue : list.Aggregate<T>(aggregateFunction);
        }


        public static DataTable Ext_ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;
            FieldInfo[] oField = null;
            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type) rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof (Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                    oField = ((Type) rec.GetType()).GetFields();
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        Type colType = fieldInfo.FieldType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof (Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(fieldInfo.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                if (oProps != null)
                {
                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                    }
                }
                if (oField != null)
                {
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        dr[fieldInfo.Name] = fieldInfo.GetValue(rec) ?? DBNull.Value;
                    }
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static IEnumerable<TDestination> Ext_Convert<TSource,
                TDestination>(this IEnumerable<TSource> enumerable,
                Func<TSource, TDestination> converter)
        {
            if (enumerable == null)
            {
                return null;
            }

            IList<TDestination> items = new List<TDestination>();
            foreach (TSource item in enumerable)
            {
                items.Add(converter(item));
            }
            return items.AsEnumerable();
        }

        public static IEnumerable<IEnumerable<T>> Ext_Split<T>(this IEnumerable<T> source, int splitSize)
        {
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return Ext_SplitInner(enumerator, splitSize);
                }
            }

        }
        private static IEnumerable<T> Ext_SplitInner<T>(IEnumerator<T> enumerator, int splitSize)
        {
            int count = 0;
            do
            {
                count++;
                yield return enumerator.Current;
            }
            while (count % splitSize != 0
                 && enumerator.MoveNext());
        }

        ///<summary>  Concatenates a specified separator String betweeneach element of a specified enumeration, yielding a single concatenatedstring. /// 
        /// </summary>
        ///  <typeparam name="T">anyobject</typeparam> 
        /// /// <param name="list">The enumeration</param> 
        /// <param name="separator">A String</param> 
        /// <returns>A String consisting of the elements of value interspersed with the separator string.</returns>
        public static string Ext_ToString<T>(this IEnumerable<T> list, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var obj in list)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(obj);
            }
            return sb.ToString();
        }

        public static IEnumerable<T> Ext_Shuffle<T>(this IEnumerable<T> list)
        {
            var r = new Random((int)DateTime.Now.Ticks);
            var shuffledList = list.Select(x => new { Number = r.Next(), Item = x })
                            .OrderBy(x => x.Number).Select(x => x.Item);
            return shuffledList.ToList();
        }


        /// <summary>  Returns the index of the first occurrence in a sequence by using the default equality comparer. 
        ///</summary> 
        /// <typeparam name="TSource">The type of the elements of source.</typeparam> 
        /// <param name="list">A sequence in which to locate a value.</param> 
        /// <param name="value">The object to locate in the sequence</param> 
        /// <returns>The zero-based index of the first occurrence of value within the entire sequence, if found; otherwise, –1.</returns>
        public static int Ext_IndexOf<TSource>(this IEnumerable<TSource> list, TSource value)
            where TSource : IEquatable<TSource>
        {
            return list.Ext_IndexOf<TSource>(value, EqualityComparer<TSource>.Default);
        }
        /// <summary> Returns the index of the first occurrence in a sequence by using aspecified IEqualityComparer. 
        /// </summary> ///
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="list">A sequence in which to locate a value.</param> ///
        ///  <param name="value">The object to locate in the sequence</param> /// <param name="comparer">An equality comparer to compare values.</param> 
        /// <returns>The zero-based index of the first occurrence of value within the entire sequence, if found; otherwise, –1.</returns>
        public static int Ext_IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
        {
            int index = 0;
            foreach (var item in list)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }


        public static IEnumerable<T> Ext_ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> Ext_Sleep<T>(this IEnumerable<T> source, int millisecondsTimeout)
        {
            foreach (T enumerable in source)
            {
                Thread.Sleep(millisecondsTimeout);
                yield return enumerable;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Ext_WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
        public static IEnumerable<TSource> Ext_WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IOrderedEnumerable<TSource> Ext_OrderBy<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> keySelector, bool descending)
        {
            if (enumerable == null) { return null; }
            if (descending)
            {
                return enumerable.OrderByDescending(keySelector);
            }
            return enumerable.OrderBy(keySelector);
        }
        public static IOrderedEnumerable<TSource> Ext_OrderBy<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, IComparable> keySelector1, Func<TSource, IComparable> keySelector2, params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }
            IEnumerable<TSource> current = enumerable;
            if (keySelectors != null)
            {
                for (int i = keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.OrderBy(keySelectors[i]);
                }
            } current = current.OrderBy(keySelector2);
            return current.OrderBy(keySelector1);
        }
        public static IOrderedEnumerable<TSource> Ext_OrderBy<TSource>(this IEnumerable<TSource> enumerable, bool descending, Func<TSource, IComparable> keySelector, params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }
            IEnumerable<TSource> current = enumerable; if (keySelectors != null)
            {
                for (int i =
                    keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.Ext_OrderBy(keySelectors[i], descending);
                }
            } return current.Ext_OrderBy(keySelector, descending);
        }
        public static IEnumerable<TResult> Ext_GetEmpty<TResult>(this IEnumerable<TResult> source)
        {
            yield break;
        }

        public static IEnumerable<TResult> Ext_Repeat<TResult>(this IEnumerable<TResult> source, int count, bool onEndOfCollection)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (onEndOfCollection)
            {
                for (int i = 0; i < count; i++)
                {

                    foreach (TResult result in source)
                    {
                        yield return result;
                    }
                }
            }

            else
            {
                foreach (TResult result in source)
                {
                    for (int i = 0; i < count; i++)
                    {
                        yield return result;
                    }
                }
            }


        }

        public static IEnumerable<int> Ext_GetInnerCount<TResult>(IEnumerable<TResult> source)
    where TResult : ICollection
        {
            foreach (TResult result in source)
            {
                yield return result.Count;
            }
        }


        public static IEnumerable<TSource> Ext_StopUntile<TSource>(this IEnumerable<TSource> dumySource, Predicate<TSource> predicate)
        {
            foreach (TSource item in dumySource)
            {
                if (predicate(item))
                {
                    yield break;
                }
                yield return item;
            }
        }


    }
}
