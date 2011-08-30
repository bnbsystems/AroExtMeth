using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class IEnumerableExt
    {
        #region NoN-GENERIC

        public static IEnumerable SelectRecursive<T>(this IEnumerable iEnumerator, Func<T, IEnumerable> iSelectManyFunc)
            where T : class
        {
            if (iEnumerator != null)
            {
                foreach (var item in iEnumerator)
                {
                    yield return item;
                    T itemT = item as T;
                    if (itemT != null)
                    {
                        var selectedElements = iSelectManyFunc(itemT);
                        var subitem = selectedElements.SelectRecursive(iSelectManyFunc);
                        foreach (var item2 in subitem)
                        {
                            yield return item2;
                        }
                    }
                }
            }
        }

        public static IList ToList<T>(this IEnumerable iEnumerator)
            where T : class
        {
            var rList = new List<T>();
            foreach (var item in iEnumerator)
            {
                T objectT = item as T;
                rList.Add(objectT);
            }

            return rList;
        }

        public static object[] ToArrayOfObjects(this IEnumerable list)
        {
            IList<object> rList = new List<object>();
            foreach (var obj in list)
            {
                rList.Add(rList);
            }
            return rList.ToArray();
        }

        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable iEnumerable)
        {
            foreach (var item in iEnumerable)
            {
                yield return (T)item;
            }
        }

        #endregion NoN-GENERIC

        #region Generic

        public static IEnumerable<T> AddIteration<T>(this IEnumerable<T> iEnumerable1, IEnumerable<T> iEnumerable2)
        {
            foreach (T variable in iEnumerable1)
            {
                yield return variable;
            }
            foreach (T variable in iEnumerable2)
            {
                yield return variable;
            }
        }

        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> iEnumerator, Func<T, IEnumerable<T>> iSelectManyFunc)
        {
            if (iEnumerator != null)
            {
                foreach (T item in iEnumerator)
                {
                    yield return item;
                    var selectedElements = iSelectManyFunc(item);
                    var subitem = selectedElements.SelectRecursive(iSelectManyFunc);
                    foreach (var item2 in subitem)
                    {
                        yield return item2;
                    }
                }
            }
        }

        public static bool All<T>(this IEnumerable<T> iEnumerator, T elementToCompare) //todo: check it
        {
            if (iEnumerator != null)
            {
                foreach (var item in iEnumerator)
                {
                    if (item.Equals(elementToCompare) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static bool All<T>(this  IEnumerable<T> iEnumerator, uint minCountNumber)// todo: check it
        {
            if (iEnumerator != null && iEnumerator.Count() > minCountNumber)
            {
                return true;
            }
            return false;
        }

        public static T Aggregate<T>(this IEnumerable<T> list, Func<T, T, T> aggregateFunction)
        {
            return Aggregate<T>(list, default(T), aggregateFunction);
        }

        public static T Aggregate<T>(this IEnumerable<T> list, T defaultValue, Func<T, T, T> aggregateFunction)
        {
            return list.Count() <= 0 ?
                defaultValue : list.Aggregate<T>(aggregateFunction);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
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
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                    oField = ((Type)rec.GetType()).GetFields();
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        Type colType = fieldInfo.FieldType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
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

        public static IEnumerable<TDestination> Convert<TSource,
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

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int splitSize)
        {
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return SplitInner(enumerator, splitSize);
                }
            }
        }

        private static IEnumerable<T> SplitInner<T>(IEnumerator<T> enumerator, int splitSize)
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
        public static string ToString<T>(this IEnumerable<T> list, string separator)
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

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
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
        public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value)
            where TSource : IEquatable<TSource>
        {
            return list.IndexOf<TSource>(value, EqualityComparer<TSource>.Default);
        }

        /// <summary> Returns the index of the first occurrence in a sequence by using aspecified IEqualityComparer.
        /// </summary> ///
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="list">A sequence in which to locate a value.</param> ///
        ///  <param name="value">The object to locate in the sequence</param> /// <param name="comparer">An equality comparer to compare values.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire sequence, if found; otherwise, –1.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
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

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> SleepForEach<T>(this IEnumerable<T> source, int millisecondsTimeout)
        {
            foreach (T enumerable in source)
            {
                Thread.Sleep(millisecondsTimeout);
                yield return enumerable;
            }
        }

        #region Stopwatch

        public static IList<Stopwatch> ToStopwatch<T>(this IEnumerable<T> source, Action<T> action)
        {
            IList<Stopwatch> stopWatches = new List<Stopwatch>();

            foreach (T enumerable in source)
            {
                Stopwatch vStopWatch = new Stopwatch();
                vStopWatch.Start();
                action(enumerable);
                vStopWatch.Stop();
                stopWatches.Add(vStopWatch);
            }
            return stopWatches;
        }

        public static IList<Stopwatch> AddedStopwatch = new List<Stopwatch>();

        public static IEnumerable<T> AddStopwatch<T>(this IEnumerable<T> source)
        {
            AddedStopwatch = new List<Stopwatch>();
            foreach (T item in source)
            {
                Stopwatch vStopWatch = new Stopwatch();
                vStopWatch.Start();
                yield return item;
                vStopWatch.Stop();
                AddedStopwatch.Add(vStopWatch);
            }
        }

        #endregion Stopwatch

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> TakeEvery<TSource>(this IEnumerable<TSource> source, int everyNumber)
        {
            if (source != null)
            {
                int vCounter = 0;
                foreach (var item in source)
                {
                    vCounter++;
                    if (vCounter == everyNumber)
                    {
                        yield return item;
                        vCounter = 0;
                    }
                }
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> keySelector, bool descending)
        {
            if (enumerable == null) { return null; }
            if (descending)
            {
                return enumerable.OrderByDescending(keySelector);
            }
            return enumerable.OrderBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, IComparable> keySelector1, Func<TSource, IComparable> keySelector2, params Func<TSource, IComparable>[] keySelectors)
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

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable, bool descending, Func<TSource, IComparable> keySelector, params Func<TSource, IComparable>[] keySelectors)
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
                    current = current.OrderBy(keySelectors[i], descending);
                }
            } return current.OrderBy(keySelector, descending);
        }

        public static IEnumerable<TResult> GetEmpty<TResult>(this IEnumerable<TResult> source)
        {
            yield break;
        }

        public static IEnumerable<TResult> Repeat<TResult>(this IEnumerable<TResult> source, int count, bool onEndOfCollection)
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

        public static IEnumerable<int> GetInnerCount<TResult>(IEnumerable<TResult> source)
    where TResult : ICollection
        {
            foreach (TResult result in source)
            {
                yield return result.Count;
            }
        }

        public static IEnumerable<TSource> StopUntile<TSource>(this IEnumerable<TSource> dumySource, Predicate<TSource> predicate)
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

        #endregion Generic
    }
}